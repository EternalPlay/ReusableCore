using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using EternalPlay.ReusableCore.Extensions;

namespace EternalPlay.ReusableCore {
    /// <summary>
    /// A simple to use object heirarchy for managing user configuration files for desktop applications.
    /// </summary>
    public sealed class UserConfig {
        #region Fields
        private string _applicationName;
        private string _configurationFilePath;
        private string _version;
        private Dictionary<string, string> _items;
        private Dictionary<string, IList<string>> _lists;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the user configuration based on the given application name and version number.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="version"></param>
        public UserConfig(string applicationName, string version) {
            _applicationName = applicationName;
            _version = version;

            LoadConfiguration(this.ConfigurationFilePath, _items = new Dictionary<string, string>(), _lists = new Dictionary<string, IList<string>>());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the application the configuration represents.
        /// </summary>
        public string ApplicationName {
            get {
                return _applicationName;
            }
        }

        /// <summary>
        /// Gets the version of the application the configuration represents.
        /// </summary>
        public string Version {
            get {
                return _version;
            }
        }

        /// <summary>
        /// Lazy loaded internal property for the path to the file that holds configuration data
        /// </summary>
        private string ConfigurationFilePath {
            get {
                return _configurationFilePath ?? (_configurationFilePath = CreateConfigurationFilePath(_applicationName, _version));
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Constructs a proper configuration file path based on the given application name and version.
        /// </summary>
        /// <param name="applicationName">Name of the application being configured.</param>
        /// <param name="version">Version of the application being configured.</param>
        /// <returns>Full path name to the application configuration file.</returns>
        private static string CreateConfigurationFilePath(string applicationName, string version) {
            return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName), string.Format(Constants.ConfigFileNameFormat, applicationName, version));
        }

        private static void LoadConfiguration(string filePath, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            XDocument configurationXml;

            if (File.Exists(filePath)) {
                try {
                    configurationXml = XDocument.Load(filePath);
                    LoadConfigurationXml(configurationXml, items, lists);
                } catch (XmlException e) {
                    //TODO:  Something with the error
                }
            }
        }

        private static void LoadConfigurationXml(XDocument configurationXml, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            LoadConfigurationItems(configurationXml, items);
            LoadConfigurationLists(configurationXml, lists);
        }

        private static void LoadConfigurationItems(XDocument configurationXml, IDictionary<string, string> items) {
            configurationXml
                .Descendants(Constants.XNameElementItem)
                .Where(element => element.Parent.Name != Constants.XNameElementList)
                .ForEach(element => {
                    items.Add(element.Attribute(Constants.XNameAttributeKey).Value, element.Value);
                });
        }

        private static void LoadConfigurationLists(XDocument configurationXml, IDictionary<string, IList<string>> lists) {
            configurationXml
                .Descendants(Constants.XNameElementList)
                .ForEach(element => {
                    lists.Add(element.Attribute(Constants.XNameAttributeKey).Value, CreateListFromItemElements(element.Descendants(Constants.XNameElementItem), new List<string>()));
                });
        }

        private static IList<string> CreateListFromItemElements(IEnumerable<XElement> itemElements, IList<string> list) {
            itemElements.ForEach(element => {
                list.Add(element.Value);
            });

            return list;
        }
        #endregion

        #region Nested Types
        private static class Constants {
            /// <summary>
            /// Format string for constructing a config file name.
            /// </summary>
            public const string ConfigFileNameFormat = "{0}.{1}.config";

            public const string CurrentConfigVersion = "1.0";

            public static readonly XName XNameAttributeApplicationName = "applicationName";

            public static readonly XName XNameAttributeConfigVersion = "configVersion";

            public static readonly XName XNameAttributeKey = "key";

            public static readonly XName XNameAttributeVersion = "version";

            public static readonly XName XNameElementItem = "item";

            public static readonly XName XNameElementList = "list";

            public static readonly XName XNameElementUserConfig = "userConfig";

            
        }
        #endregion

    }
}
