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
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web.UI;

namespace EternalPlay.ReusableCore.Web {
    /// <summary>
    /// Extension of the UriBuilder class that helps manage querystring key value pairs
    /// </summary>
	public class UrlBuilder : UriBuilder {
		#region Properties
        /// <summary>
        /// Private member for QueryString property
        /// </summary>
        IDictionary<string, object> _queryString;
        /// <summary>
        /// Gets the dictionary 
        /// </summary>
		public IDictionary<string, object> QueryString {
			get {
				if(_queryString == null) {
                    _queryString = new Dictionary<string, object>();
				}

				return _queryString;
			}
		}

        /// <summary>
        /// Gets and sets the page name for the Url
        /// </summary>
		public string PageName {
			get {
				string path = base.Path;
				return path.Substring(path.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1);
			}

			set {
				string path = base.Path;
				path = path.Substring(0, path.LastIndexOf("/", StringComparison.OrdinalIgnoreCase));
				base.Path = string.Concat(path, "/", value);
			}
		}
		#endregion
 
		#region Constructor overloads
        /// <summary>
        /// Constructs a default UrlBuilder
        /// </summary>
		public UrlBuilder() : base() {
		}
 
		/// <summary>
		/// Constructs a UrlBuilder from the given URI string
		/// </summary>
		/// <param name="uri"></param>
        public UrlBuilder(string uri) : this(new Uri(uri)) {
			PopulateQueryString();
		}
 
		/// <summary>
		/// Constructs a UrlBuilder from the given Uri object
		/// </summary>
		/// <param name="uri"></param>
        public UrlBuilder(Uri uri) : base(uri) {
			PopulateQueryString();
		}
 
		/// <summary>
		/// Constructs a UrlBuilder from a page object
		/// </summary>
		/// <param name="page"></param>
        public UrlBuilder(Page page) : this(new Uri(page.Request.Url.AbsoluteUri)) {
			PopulateQueryString();
		}
		#endregion
 
		#region Public methods
        /// <summary>
        /// Gets a string representation of the constructed Url
        /// </summary>
        /// <returns></returns>
		public new string ToString() {
			BuildQuery();  //NOTE:  build the query on the base UriBuilder
 
			return base.Uri.AbsoluteUri;
		}

        /// <summary>
        /// Navigates to the constructed Url, ending the current response stream
        /// </summary>
		public void Navigate() {
            Redirect(true);
		}
 
        /// <summary>
        /// Navigates to the constructed Url, optionally ending the current response stream
        /// </summary>
        /// <param name="endResponse">Boolean indicating of the current response stream should be terminated</param>
		public void Navigate(bool endResponse) {
            Redirect(endResponse);
		}
  		#endregion
 
		#region Functions
        /// <summary>
        /// Private method that converts the internal query string dictionary into a uri query
        /// </summary>
        private void BuildQuery() {
            int count = this.QueryString.Count;

            if (count == 0) {
                base.Query = string.Empty;
                return;
            }

            string[] keys = new string[count];
            object[] values = new object[count];
            string[] pairs = new string[count];

            this.QueryString.Keys.CopyTo(keys, 0);
            this.QueryString.Values.CopyTo(values, 0);

            for (int i = 0; i < count; i++) {
                pairs[i] = string.Concat(keys[i], "=", values[i].ToString());
            }

            //NOTE:  Set the base Uri builder query
            base.Query = string.Join("&", pairs);
        }

		/// <summary>
		/// Populates the internal dictionary from the internal uri.
		/// </summary>
        private void PopulateQueryString() {
			string query = base.Query;

            if (string.IsNullOrEmpty(query))
                return;

            this.QueryString.Clear();

			query = query.Substring(1); //remove the ?
 
			string[] pairs = query.Split(new char[]{'&'});
			foreach(string rawKeyValuePair in pairs) {
                string[] keyValuePair = rawKeyValuePair.Split(new char[] { '=' });

                string key = keyValuePair[0];
                string value = (keyValuePair.Length > 1) ? keyValuePair[1] : string.Empty;
                
                this.QueryString.Add(new KeyValuePair<string, object>(key, value));
			}
		}

        /// <summary>
        /// Reusable function for executing a response redirect to the internal uri
        /// </summary>
        /// <param name="endResponse"></param>
        private void Redirect(bool endResponse) {
            string uri = this.ToString();
            HttpContext.Current.Response.Redirect(uri, endResponse);
        }
		#endregion
	}
}
