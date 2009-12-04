using EternalPlay.ReusableCore.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EternalPlay.ReusableCore.Web.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for EntryExceptionTest and is intended
    ///to contain all EntryExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntryExceptionTest {


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
        ///A test for EntryException Constructor
        ///</summary>
        [TestMethod()]
        public void EntryExceptionConstructorEmptyTest() {
            EntryException target = new EntryException();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for EntryException Constructor
        ///</summary>
        [TestMethod()]
        public void EntryExceptionConstructorMessageTest() {
            string expected = "Exception Message";
            EntryException target = new EntryException(expected);
            Assert.IsNotNull(target);

            string actual = target.Message;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EntryException Constructor
        ///</summary>
        [TestMethod()]
        public void EntryExceptionConstructorMessageInnerExceptionTest() {
            string expectedMessage = "Exception Message";
            Exception expectedInnerException = new NotImplementedException();
            EntryException target = new EntryException(expectedMessage, expectedInnerException);
            Assert.IsNotNull(target);

            string actualMessage = target.Message;
            Assert.AreEqual(expectedMessage, actualMessage);

            Exception actualInnerException = target.InnerException;
            Assert.AreEqual(expectedInnerException, actualInnerException);
        }

        /// <summary>
        ///A test for EntryException Constructor
        ///</summary>
        [TestMethod()]
        public void EntryExceptionConstructorSerializationTest() {
            EntryException source, target;
            source = new EntryException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (EntryException)formatter.Deserialize(formatStream);
            }

            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetObjectData
        ///</summary>
        [TestMethod()]
        public void EntryExceptionGetObjectDataTest() {
            EntryException source, target;
            source = new EntryException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (EntryException)formatter.Deserialize(formatStream); //NOTE:  This will cause a call to GetObjectData
            }

            Assert.IsNotNull(target);
        }
    }
}