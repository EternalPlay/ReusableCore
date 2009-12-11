using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EternalPlay.ReusableCore {
    /// <summary>
    /// A simple to use object heirarchy for managing user configuration files for desktop applications.
    /// </summary>
    public sealed class UserConfig {
        #region Fields
        private string _applicationName;
        private string _version;
        private string _configurationFilePath;
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
        #endregion

        #region Nested Types
        private static class Constants {
            /// <summary>
            /// Format string for constructing a config file name.
            /// </summary>
            public const string ConfigFileNameFormat = "{0}.{1}.config";
        }
        #endregion

    }
}
