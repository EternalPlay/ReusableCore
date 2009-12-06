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
using System.Web.UI;

[assembly: WebResource("EternalPlay.ReusableCore.Web.ContentPopup.js", "text/javascript")]
[assembly: WebResource("EternalPlay.ReusableCore.Web.CrossBrowserFunctions.js", "text/javascript")]
[assembly: WebResource("EternalPlay.ReusableCore.Web.Expander.js", "text/javascript")]
[assembly: WebResource("EternalPlay.ReusableCore.Web.EntryControls.js", "text/javascript")]

namespace EternalPlay.ReusableCore.Web {

    /// <summary>
    /// Used to consolidate access to control resources
    /// </summary>
    public static class ControlScriptResources {

        #region Constants

        private const string webResourceName_CrossBrowserFunctions = "EternalPlay.ReusableCore.Web.JavaScript.CrossBrowserFunctions.js";
        private const string webResourceName_ContentPopup = "EternalPlay.ReusableCore.Web.JavaScript.ContentPopup.js";
        private const string webResourceName_Expander = "EternalPlay.ReusableCore.Web.JavaScript.Expander.js";
        private const string webResourceName_EntryControls = "EternalPlay.ReusableCore.Web.JavaScript.EntryControls.js";

        #endregion

        #region Methods

        /// <summary>
        /// Handles the registration of client script resources
        /// </summary>
        /// <param name="resourceItem">Resource item to be registered</param>
        /// <param name="control">Control requesting the registration</param>
        public static void RegisterScriptResource(ClientScriptType resourceItem, Control control) {

            //NOTE:  Validate arguments of public methods 
            if (control == null)
                throw new ArgumentNullException("control");

            //NOTE:  Resolve the resource item into the resource name
            string resourceName = String.Empty;
            switch (resourceItem) {
                case ClientScriptType.ContentPopup:
                    resourceName = ControlScriptResources.webResourceName_ContentPopup;
                    break;
                case ClientScriptType.CrossBrowserFunctions:
                    resourceName = ControlScriptResources.webResourceName_CrossBrowserFunctions;
                    break;
                case ClientScriptType.EntryControls:
                    resourceName = ControlScriptResources.webResourceName_EntryControls;
                    break;
                case ClientScriptType.Expander:
                    resourceName = ControlScriptResources.webResourceName_Expander;
                    break;
            }

            //NOTE:  Register the client script resource
            control.Page.ClientScript.RegisterClientScriptResource(typeof(ControlScriptResources), resourceName);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Resolve the resource item into the resource name
        /// </summary>
        /// <param name="resourceItem">Resource item that the name is retrieved for</param>
        /// <returns>Resource name string</returns>
        private static string ResolveScriptItemIntoResourceName(ClientScriptType resourceItem) {
            switch (resourceItem) {
                case ClientScriptType.ContentPopup:
                    return ControlScriptResources.webResourceName_ContentPopup;
                case ClientScriptType.CrossBrowserFunctions:
                    return ControlScriptResources.webResourceName_CrossBrowserFunctions;
                case ClientScriptType.EntryControls:
                    return ControlScriptResources.webResourceName_EntryControls;
                case ClientScriptType.Expander:
                    return ControlScriptResources.webResourceName_Expander;
                default:
                    return String.Empty;
            }
        }

        #endregion

    }
}
