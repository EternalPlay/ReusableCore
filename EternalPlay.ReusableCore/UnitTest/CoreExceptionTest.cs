using EternalPlay.ReusableCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EternalPlay.ReusableCore.Web.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CoreExceptionTest and is intended
    ///to contain all CoreExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoreExceptionTest {


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

        /// <summary>
        ///A test for CoreException Constructor
        ///</summary>
        [TestMethod()]
        public void CoreExceptionConstructorEmptyTest() {
            CoreException target = new CoreException();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CoreException Constructor
        ///</summary>
        [TestMethod()]
        public void CoreExceptionConstructorMessageTest() {
            string expected = "Exception Message";
            CoreException target = new CoreException(expected);
            Assert.IsNotNull(target);

            string actual = target.Message;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CoreException Constructor
        ///</summary>
        [TestMethod()]
        public void CoreExceptionConstructorMessageInnerExceptionTest() {
            string expectedMessage = "Exception Message";
            Exception expectedInnerException = new NotImplementedException();
            CoreException target = new CoreException(expectedMessage, expectedInnerException);
            Assert.IsNotNull(target);

            string actualMessage = target.Message;
            Assert.AreEqual(expectedMessage, actualMessage);

            Exception actualInnerException = target.InnerException;
            Assert.AreEqual(expectedInnerException, actualInnerException);
        }

        /// <summary>
        ///A test for CoreException Constructor
        ///</summary>
        [TestMethod()]
        public void CoreExceptionConstructorSerializationTest() {
            CoreException source, target;
            source = new CoreException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (CoreException)formatter.Deserialize(formatStream);
            }

            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetObjectData
        ///</summary>
        [TestMethod()]
        public void CoreExceptionGetObjectDataTest() {
            CoreException source, target;
            source = new CoreException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (CoreException)formatter.Deserialize(formatStream); //NOTE:  This will cause a call to GetObjectData
            }

            Assert.IsNotNull(target);
        }
    }
}