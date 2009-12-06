#region License
/*	
Microsoft Reciprocal License (Ms-RL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) Reciprocal Grants- For any file you distribute that contains code from the software (in source code or binary format), you must provide recipients the source code to that file along with a copy of this license, which license will govern that file. You may license other files that are entirely your own work and do not contain code from the software under any terms you choose.
(B) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(C) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(D) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(E) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(F) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EternalPlay.ReusableCore.Web {

    /// <summary>
    /// Encapsulates functionality that eases client side scripting by providing easy references to server controls (automatically resolves clientIDs)
    /// </summary>
    [ToolboxData("<{0}:ControlIdentification runat=server></{0}:ControlIdentification>")]
    [Themeable(false)]
    public class ControlIdentification : WebControl {

        #region Fields

        private Dictionary<string, string> registeredControls = new Dictionary<string, string>();

        #endregion

        #region Properties

        /// <summary>
        /// Name used for the client side object holding control references.
        /// </summary>
        /// <example>ControlReference.MyTextBox.select();</example>
        [Bindable(false)]
        [Category("Behavior")]
        [DefaultValue("ControlReference")]
        [Localizable(false)]
        public string Namespace {
            get {
                return (String)ViewState["Namespace"] ?? "ControlReference";
            }

            set {
                ViewState["Namespace"] = value;
            }
        }

        /// <summary>
        /// Returns true if there are any controls registered
        /// </summary>
        public bool HasRegisteredControls {
            get {
                return (registeredControls.Count > 0);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers a control for client side reference by server ID ensuring that only one control with a given ID is registered at a time.
        /// </summary>
        /// <param name="controlReference">Reference to the control to be registered</param>
        /// <remarks>If a control is registered multiple times with the same ID each time, only the last registration will remain.  They overwrite eachother so that each ID for registered controls is unique.</remarks>
        public void Register(Control controlReference) {

            //NOTE:  Validate arguments of public methods 
            if (controlReference == null)
                throw new ArgumentNullException("controlReference");

            this.Register(controlReference.ID, controlReference.ClientID);
        }

        /// <summary>
        /// Registers an entry control for client side reference by server ID ensuring that only one control with a given ID is registered at a time.
        /// </summary>
        /// <param name="entryControl">Reference to any entry control derived from EntryBase</param>
        /// <remarks>The ID is taken from the entry control and the client id is taken from the contained child text box control</remarks>
        public void Register(EntryBase entryControl) {

            //NOTE:  Validate arguments of public methods 
            if (entryControl == null)
                throw new ArgumentNullException("entryControl");

            this.Register(entryControl.ID, entryControl.TextBoxClientId);
        }

        /// <summary>
        /// Registers a control for client side reference by server ID ensuring that only one control with a given ID is registered at a time.
        /// </summary>
        /// <param name="controlId">The server side id of the control to be registered</param>
        /// <param name="controlClientId">The client side id fo the control to be registered</param>
        public void Register(string controlId, string controlClientId) {
            //NOTE:  Remove preexistent control
            if (registeredControls.ContainsKey(controlId))
                registeredControls.Remove(controlId);

            //NOTE:  Add the control fresh
            registeredControls.Add(controlId, controlClientId);
        }

        /// <summary>
        /// Find a server side control by walking up the naming containers
        /// </summary>
        /// <param name="targetId">ID of server side control to locate</param>
        /// <param name="startControl">Reference to the naming container to start searching within - progress will move from this naming container until it reaches the page</param>
        /// <returns>Reference to located server side control or null</returns>
        public static Control DeepFindControl(string targetId, Control startControl) {
            Control namingContainer = startControl, targetControl;

            do {
                targetControl = namingContainer.FindControl(targetId);
                if (targetControl != null)
                    return targetControl;
                else
                    namingContainer = namingContainer.NamingContainer;
            } while (!(namingContainer is Page));

            return null;
        }

        #endregion

        #region Events

        /// <summary>
        /// Render Javascript block to client.  This only happens once for all controls on the page.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer) {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.Write(BuildControlIdentificationJavaScript());
            writer.RenderEndTag();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Builds the javascript declaring and instantiating the control identification references
        /// </summary>
        /// <returns>JavaScript string</returns>
        private string BuildControlIdentificationJavaScript() {
            StringBuilder scriptBuilder = new StringBuilder();

            string baseNamespaceInit = "if (typeof({0}) == 'undefined') {0} = new Object();";
            scriptBuilder.Append(String.Format(CultureInfo.InvariantCulture, baseNamespaceInit, this.Namespace));

            string baseDeclaration = "{0}.{1} = document.getElementById('{2}');\n";
            foreach (KeyValuePair<string, string> pair in registeredControls)
                scriptBuilder.Append(String.Format(CultureInfo.InvariantCulture, baseDeclaration, this.Namespace, pair.Key, pair.Value));

            return scriptBuilder.ToString();
        }

        #endregion

    }
}
