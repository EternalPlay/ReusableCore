using EternalPlay.ReusableCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EternalPlay.ReusableCore.UnitTest {

    /// <summary>
    ///This is a test class for UserConfigTest and is intended
    ///to contain all UserConfigTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserConfigTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        #region Constructor Tests
        /// <summary>
        ///A test for UserConfig Constructor
        ///</summary>
        [TestMethod()]
        public void UserConfigConstructorTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig target = new UserConfig(applicationName, version);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for UserConfig Constructor
        ///</summary>
        [TestMethod()]
        public void UserConfigConstructorNullApplicationTest() {
            string applicationName = string.Empty;
            string version = "1.0.0.0";

            try {
                UserConfig target = new UserConfig(applicationName, version);
                Assert.Fail("Constructing with an empty application should have thrown an error.");
            } catch (ArgumentNullException e) {
                Assert.IsInstanceOfType(e, typeof(ArgumentNullException));
            }
        }
        #endregion

        #region Property Tests
        /// <summary>
        ///A test for ApplicationName
        ///</summary>
        [TestMethod()]
        public void UserConfigApplicationNameTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig target = new UserConfig(applicationName, version);
            string actual = target.ApplicationName;

            Assert.AreEqual(applicationName, actual);

        }

        /// <summary>
        ///A test for ConfigurationFilePath
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigConfigurationFilePathTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig_Accessor target = new UserConfig_Accessor(applicationName, version);
            string actual = target.ConfigurationFilePath;
            string expected = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName), string.Format("{0}.{1}.config", applicationName, version));

            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Version
        ///</summary>
        [TestMethod()]
        public void UserConfigVersionTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig target = new UserConfig(applicationName, version);
            string actual = target.Version;

            Assert.AreEqual(version, actual);
        }
        #endregion

        #region Methods
        /// <summary>
        ///A test for GetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigGetItemTest() {
            string filePath = "SampleConfig.xml";
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig_Accessor target = new UserConfig_Accessor(applicationName, version);
            UserConfig_Accessor.LoadConfiguration(filePath, target._items = new Dictionary<string, string>(), target._lists = new Dictionary<string, IList<string>>());

            string key = "item1";
            string defaultValue = string.Empty;
            string actual, expected;
            expected = "value1";
            actual = target.GetItem(key, defaultValue);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigGetItemDataTypesTest() {
            string filePath = "SampleConfigDataTypes.xml";
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig_Accessor target = new UserConfig_Accessor(applicationName, version);
            UserConfig_Accessor.LoadConfiguration(filePath, target._items = new Dictionary<string, string>(), target._lists = new Dictionary<string, IList<string>>());

            string key = "boolFalse";
            string defaultValue = "True";
            bool actual, expected;
            expected = false;
            actual = bool.Parse(target.GetItem(key, defaultValue));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigGetItemDefaultTest() {
            string filePath = "SampleConfig.xml";
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig_Accessor target = new UserConfig_Accessor(applicationName, version);
            UserConfig_Accessor.LoadConfiguration(filePath, target._items = new Dictionary<string, string>(), target._lists = new Dictionary<string, IList<string>>());

            string key = "doesnotexist";
            string defaultValue = "defaultvalue";
            string actual, expected;
            expected = defaultValue;
            actual = target.GetItem(key, defaultValue);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        [TestMethod()]
        public void UserConfigGetListTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "itemkey";
            IList<string> list = new List<string>();
            target.SetList(key, list);

            IList<string> expected = list;
            IList<string> actual = target.GetList(key, null);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        [TestMethod()]
        public void UserConfigGetListDefaultTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "doesnotexist";
            IList<string> defaultlist = new List<string>();

            target.GetList(key, defaultlist);
            IList<string> expected = defaultlist;
            IList<string> actual = target.GetList(key, defaultlist);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigSaveTest() {
            string filePath = "SampleConfig.xml";
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";

            UserConfig_Accessor target = new UserConfig_Accessor(applicationName, version);
            UserConfig_Accessor.LoadConfiguration(filePath, target._items = new Dictionary<string, string>(), target._lists = new Dictionary<string, IList<string>>());

            target.Save();

            UserConfig_Accessor postSaveTarget = new UserConfig_Accessor(applicationName, version);
            Assert.AreEqual(3, postSaveTarget._items.Count);
            Assert.AreEqual(1, postSaveTarget._lists.Count);

            UserConfig_Accessor.DeleteConfigurationFile(postSaveTarget._configurationFilePath);
        }

        /// <summary>
        ///A test for SetList
        ///</summary>
        [TestMethod()]
        public void UserConfigSetListNewTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "itemkey";
            IList<string> list = new List<string>();
            target.SetList(key, list);

            IList<string> expected = list;
            IList<string> actual = target.GetList(key, null);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetList
        ///</summary>
        [TestMethod()]
        public void UserConfigSetListExistingTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "itemkey";
            IList<string> list = new List<string>();
            target.SetList(key, list);
            
            //NOTE:  Update list
            IList<string> newList = new List<string>();
            target.SetList(key, newList);

            IList<string> expected = newList;
            IList<string> actual = target.GetList(key, null);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetItem
        ///</summary>
        [TestMethod()]
        public void UserConfigSetItemNewTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "itemkey";
            string item = "itemValue";
            target.SetItem(key, item);

            string expected = item;
            string actual = target.GetItem(key, string.Empty);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetItem
        ///</summary>
        [TestMethod()]
        public void UserConfigSetItemExistingTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            UserConfig target = new UserConfig(applicationName, version);
            string key = "itemkey";
            string item = "itemValue";
            target.SetItem(key, item);

            //NOTE:  Set a new value for existing key
            string newItem = "newItemValue";
            target.SetItem(key, newItem);

            string expected = newItem;
            string actual = target.GetItem(key, string.Empty);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Functions
        /// <summary>
        ///A test for CreateListFromItemElements
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigCreateListFromItemElementsTest() {
            XDocument configurationXml = XDocument.Load("SampleConfig.xml");
            IEnumerable<XElement> itemElements = configurationXml.Descendants("list").Descendants("item");
            IList<string> list = new List<string>();
            IList<string> actual = UserConfig_Accessor.CreateListFromItemElements(itemElements, list);

            Assert.AreEqual(list, actual);
        }

        /// <summary>
        ///A test for CreateConfigurationFilePath
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigCreateConfigurationFilePathTest() {
            string applicationName = "Core Unit Test";
            string version = "1.0.0.0";
            string expected = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName), string.Format("{0}.{1}.config", applicationName, version));
            string actual = UserConfig_Accessor.CreateConfigurationFilePath(applicationName, version);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CreateListFromItemElements
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigEnsureConfigurationFolderTest() {
            string tempPath = Path.Combine(Path.GetTempPath(), "Core Unit Test");

            UserConfig_Accessor.EnsureConfigurationFolder(tempPath); //NOTE:  First call tests path 1, directory does not exsit
            UserConfig_Accessor.EnsureConfigurationFolder(tempPath); //NOTE:  Second call tests path 2, directory exists

            Directory.Delete(tempPath);
        }

        /// <summary>
        ///A test for LoadConfiguration using a valid sample xml file
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationTest() {
            string filePath = "SampleConfig.xml";
            IDictionary<string, string> items = new Dictionary<string, string>();
            IDictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();
            UserConfig_Accessor.LoadConfiguration(filePath, items, lists);

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(1, lists.Count);
            Assert.AreEqual(3, lists["list1"].Count);
        }

        /// <summary>
        ///A test for LoadConfiguration using valid xml that is not configuration data
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationNonConfigTest() {
            string filePath = "SampleXmlNonConfig.xml";
            IDictionary<string, string> items = new Dictionary<string, string>();
            IDictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();
            UserConfig_Accessor.LoadConfiguration(filePath, items, lists);

            Assert.AreEqual(0, items.Count);
            Assert.AreEqual(0, lists.Count);
        }

        /// <summary>
        ///A test for LoadConfiguration using valid xml that is not configuration data
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationNonXmlTest() {
            string filePath = "SampleConfigNonXml.xml";
            IDictionary<string, string> items = new Dictionary<string, string>();
            IDictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();
            UserConfig_Accessor.LoadConfiguration(filePath, items, lists);

            Assert.AreEqual(0, items.Count);
            Assert.AreEqual(0, lists.Count);
        }

        /// <summary>
        ///A test for LoadConfigurationXml
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationFromXmlTest() {
            XDocument configurationXml = XDocument.Load("SampleConfig.xml");
            IDictionary<string, string> items = new Dictionary<string, string>();
            IDictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();
            UserConfig_Accessor.LoadConfigurationFromXml(configurationXml, items, lists);

            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(1, lists.Count);
            Assert.AreEqual(3, lists["list1"].Count);
        }

        /// <summary>
        ///A test for LoadConfigurationLists
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationListsTest() {
            XDocument configurationXml = XDocument.Load("SampleConfig.xml");
            IDictionary<string, IList<string>> lists = new Dictionary<string, IList<string>>();
            UserConfig_Accessor.LoadConfigurationLists(configurationXml, lists);

            Assert.AreEqual(1, lists.Count);
            Assert.AreEqual(3, lists["list1"].Count);
        }

        /// <summary>
        ///A test for LoadConfigurationItems
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void UserConfigLoadConfigurationItemsTest() {
            XDocument configurationXml = XDocument.Load("SampleConfig.xml");
            IDictionary<string, string> items = new Dictionary<string, string>();
            UserConfig_Accessor.LoadConfigurationItems(configurationXml, items);

            Assert.AreEqual(3, items.Count);
        }
        #endregion
    }
}