using EternalPlay.ReusableCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
namespace EternalPlay.ReusableCore.UnitTest
{
    
    
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

        #region Functions
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
        #endregion
    }
}