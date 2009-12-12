using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using EternalPlay.ReusableCore.Extensions;
using System.Globalization;

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
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException("applicationName", "applicationName cannot be null or empty.");

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

        #region Methods
        /// <summary>
        /// Gets the configured value for the given key, or a default value if no value is configured
        /// </summary>
        /// <param name="key">Key of the configured item to retrieve.</param>
        /// <param name="defaultValue">Default value to use if no configured value is found for the given key.</param>
        /// <returns>String representation of the configured item.</returns>
        public string GetItem(string key, string defaultValue) {
            return (_items.ContainsKey(key)) ? _items[key] : defaultValue;
        }

        /// <summary>
        /// Gets the configured list for the given key, or a default list if no value is configured
        /// </summary>
        /// <param name="key">Key of the configured list to retrieve.</param>
        /// <param name="defaultList">Default list to use if no configured value is found for the given key.</param>
        /// <returns>Configured list.</returns>
        public IList<string> GetList(string key, IList<string> defaultList) {
            return (_lists.ContainsKey(key)) ? _lists[key] : defaultList;
        }

        /// <summary>
        /// Saves the current configuration information
        /// </summary>
        public void Save() {
            SaveConfigurationXml(_configurationFilePath, _applicationName, _version, _items, _lists);
        }

        /// <summary>
        /// Configures the given key with the given value
        /// </summary>
        /// <param name="key">Key for the configured item.</param>
        /// <param name="value">Value for the configured item</param>
        public void SetItem(string key, object value) {
            _items.Add(key, value.ToString());
        }

        /// <summary>
        /// Configures the given key with a list of strings.
        /// </summary>
        /// <param name="key">Key for the configured list.</param>
        /// <param name="list">List of strings</param>
        public void SetList(string key, IList<string> list) {
            _lists.Add(key, list);
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
            return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName), string.Format(CultureInfo.InvariantCulture, Constants.ConfigFileNameFormat, applicationName, version));
        }

        /// <summary>
        /// Loads a configuration from the file at the given file path into the given items and lists dictionaries.
        /// </summary>
        /// <param name="filePath">Path to a configuraton file.</param>
        /// <param name="items">Items collection to populate from the configuration file.</param>
        /// <param name="lists">Lists collection to populate from the configuration file.</param>
        private static void LoadConfiguration(string filePath, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            XDocument configurationXml;

            if (File.Exists(filePath)) {
                try {
                    configurationXml = XDocument.Load(filePath);
                    LoadConfigurationFromXml(configurationXml, items, lists);
                } catch (XmlException) {
                    //NOTE:  Any other exceptions should bubble through
                }
            }
        }

        /// <summary>
        /// Loads configuration information from an xml document
        /// </summary>
        /// <param name="configurationXml">XDocument xml document containing configuration information.</param>
        /// <param name="items">Items collection to populate from the configuration xml.</param>
        /// <param name="lists">Lists collection to populate from the configuration xml.</param>
        private static void LoadConfigurationFromXml(XDocument configurationXml, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            LoadConfigurationItems(configurationXml, items);
            LoadConfigurationLists(configurationXml, lists);
        }

        /// <summary>
        /// Loads an items collection from an xml document.
        /// </summary>
        /// <param name="configurationXml">XDocument xml document containing configuration information.</param>
        /// <param name="items">Items collection to populate from the configuration xml.</param>
        private static void LoadConfigurationItems(XDocument configurationXml, IDictionary<string, string> items) {
            configurationXml
                .Descendants(Constants.XNameElementItem)
                .Where(element => element.Parent.Name != Constants.XNameElementList)
                .ForEach(element => {
                    items.Add(element.Attribute(Constants.XNameAttributeKey).Value, element.Value);
                });
        }

        /// <summary>
        /// Loads a list collection from an xml document.
        /// </summary>
        /// <param name="configurationXml">XDocument xml document containing configuration information.</param>
        /// <param name="lists">Lists collection to populate from the configuration xml.</param>
        private static void LoadConfigurationLists(XDocument configurationXml, IDictionary<string, IList<string>> lists) {
            configurationXml
                .Descendants(Constants.XNameElementList)
                .ForEach(element => {
                    lists.Add(element.Attribute(Constants.XNameAttributeKey).Value, CreateListFromItemElements(element.Descendants(Constants.XNameElementItem), new List<string>()));
                });
        }

        /// <summary>
        /// Creats a list of items from a collection of xml elements.
        /// </summary>
        /// <param name="itemElements">Enumerable collection of item elements.</param>
        /// <param name="list">List to populate.</param>
        /// <returns>Returns the populated version of the list.</returns>
        private static IList<string> CreateListFromItemElements(IEnumerable<XElement> itemElements, IList<string> list) {
            itemElements.ForEach(element => {
                list.Add(element.Value);
            });

            return list;
        }

        #region Save Functions
        private static XElement CreateApplicationElement(string applicationName, string version) {
            XElement applicationElement = new XElement(Constants.XNameElementApplication);
            applicationElement.SetAttributeValue(Constants.XNameAttributeApplicationName, applicationName);
            applicationElement.SetAttributeValue(Constants.XNameAttributeVersion, version);

            return applicationElement;
        }

        private static XElement CreateDocumentRoot() {
            XElement documentRoot = new XElement(Constants.XNameElementUserConfig);
            documentRoot.SetAttributeValue(Constants.XNameAttributeConfigVersion, Constants.CurrentConfigVersion);

            return documentRoot;
        }

        private static IEnumerable<XElement> CreateItemElements(IDictionary<string, string> items) {
            return items.Select(keyValuePair => {
                XElement itemElement = new XElement(Constants.XNameElementItem);
                itemElement.SetAttributeValue(Constants.XNameAttributeKey, keyValuePair.Key);
                itemElement.Value = keyValuePair.Value;

                return itemElement;
            });
        }

        private static IEnumerable<XElement> CreateListItemElements(IList<string> items) {
            return items.Select(item => {
                XElement itemElement = new XElement(Constants.XNameElementItem);
                itemElement.Value = item;

                return itemElement;
            });
        }

        private static IEnumerable<XElement> CreateListElements(IDictionary<string, IList<string>> lists) {
            return lists.Select(keyValuePair => {
                XElement listElement = new XElement(Constants.XNameElementList);
                listElement.SetAttributeValue(Constants.XNameAttributeKey, keyValuePair.Key);
                listElement.Add(CreateListItemElements(keyValuePair.Value));

                return listElement;
            });
        }

        private static XDocument CreateSaveXml(string applicationName, string version, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            XElement documentRoot = CreateDocumentRoot();
            documentRoot.Add(CreateApplicationElement(applicationName, version));
            documentRoot.Add(CreateItemElements(items));
            documentRoot.Add(CreateListElements(lists));

            XDocument saveXml = new XDocument();
            saveXml.Add(documentRoot);

            return saveXml;
        }

        private static void DeleteConfigurationFile(string filePath) {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private static void EnsureConfigurationFolder(string folderPath) {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        private static void SaveConfigurationXml(string filePath, string applicationName, string version, IDictionary<string, string> items, IDictionary<string, IList<string>> lists) {
            DeleteConfigurationFile(filePath);
            EnsureConfigurationFolder(Path.GetDirectoryName(filePath));
            XDocument saveXml = CreateSaveXml(applicationName, version, items, lists);
            saveXml.Save(filePath);
        }
        #endregion
        #endregion

        #region Nested Types
        private static class Constants {
            /// <summary>
            /// Format string for constructing a config file name.
            /// </summary>
            public const string ConfigFileNameFormat = "{0}.{1}.config";

            /// <summary>
            /// Current version of the configuration processor
            /// </summary>
            public const string CurrentConfigVersion = "1.0";

            /// <summary>
            /// XName for application name attribute
            /// </summary>
            public static readonly XName XNameAttributeApplicationName = "applicationName";

            /// <summary>
            /// XName for configuration version attribute
            /// </summary>
            public static readonly XName XNameAttributeConfigVersion = "configVersion";

            /// <summary>
            /// XName for key attributes
            /// </summary>
            public static readonly XName XNameAttributeKey = "key";

            /// <summary>
            /// XName for version attributes
            /// </summary>
            public static readonly XName XNameAttributeVersion = "version";

            public static readonly XName XNameElementApplication = "application";

            /// <summary>
            /// XName for item elements
            /// </summary>
            public static readonly XName XNameElementItem = "item";

            /// <summary>
            /// XName for list elements
            /// </summary>
            public static readonly XName XNameElementList = "list";

            /// <summary>
            /// XName for user config elements
            /// </summary>
            public static readonly XName XNameElementUserConfig = "userConfig";
        }
        #endregion

    }
}
