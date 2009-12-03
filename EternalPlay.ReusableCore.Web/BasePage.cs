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
using System.Web;

namespace EternalPlay.ReusableCore.Web {

    /// <summary>
    /// Providing basic page functionality.  Intended to be derived by pages used in web applications.
    /// </summary>
    public abstract class BasePage : Page, IControlIdentification {

        #region Events
        /// <summary>
        /// Page load cycle event when data is bound to controls
        /// </summary>
        public event EventHandler<EventArgs> BindData;
        #endregion


        #region Fields

        private ControlIdentification controlId;

        #endregion

        #region Event Handlers
        /// <summary>
        /// Perform post initialization specific activities
        /// 
        /// 1. Bind Data
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitComplete(EventArgs e) {
            base.OnInit(e);

            //NOTE:  1. Bind Data and Lookup Groups
            OnBindData(new EventArgs());
        }

        /// <summary>
        /// Perform Data Binding Activities
        /// 
        /// 1.  Gluv List Binding
        /// 2.  General Control Binding
        /// </summary>
        protected virtual void OnBindData(EventArgs e) {
            EventHandler<EventArgs> bindDataHandler = BindData;

            if (bindDataHandler != null) {
                bindDataHandler(this, e);
            }

            return;
        }

        /// <summary>
        /// Perform activities that can wait until pre-render
        /// 
        /// 1. Assign resource strings
        /// 2. Register controls with control identification
        /// 3. Place focus on a control
        /// 4. Populate UI controls with values
        /// 5. Enable or disable controls
        /// 6. Prepare client side
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            //NOTE: 1. Assign Resource Strings

            //NOTE: 2. Register Controls with Control Identification

            //NOTE: 3. Place focus on a control (Page Specific)

            //NOTE: 4. Populate UI controls with values

            //NOTE: 5. Enable or disable controls

            //NOTE: 6. Prepare client side
            this.Page.Title = RetrievePageTitle();
        }

        /// <summary>
        /// Handle render activities unique to base page
        /// 
        /// 1.  Add the Control Identification object to the end of the controls collection if it is populated.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer) {

            //NOTE:  1.  Add the Control Identification object to the end of the controls collection if it is populated.
            if ((controlId != null) && (controlId.HasRegisteredControls))
                this.Controls.Add(controlId);

            base.Render(writer);    //NOTE:  This should be called at the end of this handler
        }

        #endregion

        #region IControlIdentification Members

        /// <summary>
        /// Returns a reference to the Control Identification server control.  Lazy loads.
        /// </summary>
        public ControlIdentification ControlId {
            get {
                //NOTE:  Lazy Load
                return controlId ?? (controlId = new ControlIdentification());
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Utility function for derived pages that will create a UrlBuilder for the given relative path.
        /// </summary>
        /// <param name="relativePath">Page url as a relative path.</param>
        /// <returns>UrlBuilder with an absolute Uri</returns>
        protected static UrlBuilder CreatePageUrlBuilder(string relativePath) {
            Uri baseUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty));
            Uri relativeUri = new Uri(HttpContext.Current.Request.ApplicationPath + relativePath, UriKind.Relative);

            return new UrlBuilder(new Uri(baseUri, relativeUri));
        }

        /// <summary>
        /// Re-initiates data binding sequence for the page heirarchy
        /// </summary>
        protected void RebindData() {
            this.OnBindData(new EventArgs());
        }

        /// <summary>
        /// Replaces a leading tilde with the application path, if the application path is not "/" root.
        /// </summary>
        /// <param name="path">path to be resolved</param>
        /// <returns>path string</returns>
        public string ResolveApplicationPath(string path) {

            //NOTE:  Validate arguments of public methods 
            if (path == null)
                throw new ArgumentNullException("path");

            if (path.StartsWith("~", StringComparison.OrdinalIgnoreCase))
                return (Request.ApplicationPath.Length > 1) ? Request.ApplicationPath + path.TrimStart('~') : path.TrimStart('~');
            return path;
        }
        #endregion

        #region Functions

        /// <summary>
        /// Retrieve Page Title from site map or use default of file name
        /// </summary>
        /// <returns>page title</returns>
        private string RetrievePageTitle() {
            string pageTitle = Request.Path.ToString();
            string[] filepath = pageTitle.Split(new char[1] { '/' });

            pageTitle = filepath[filepath.Length - 1];
            if (SiteMap.CurrentNode != null)
                pageTitle = SiteMap.CurrentNode.Title;

            return pageTitle;
        }

        #endregion

    }
}
