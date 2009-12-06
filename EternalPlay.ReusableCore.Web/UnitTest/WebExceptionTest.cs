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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EternalPlay.ReusableCore.Web.UnitTest {

    /// <summary>
    ///This is a test class for WebExceptionTest and is intended
    ///to contain all WebExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WebExceptionTest {


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
        ///A test for WebException Constructor
        ///</summary>
        [TestMethod()]
        public void WebExceptionConstructorEmptyTest() {
            WebException target = new WebException();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for WebException Constructor
        ///</summary>
        [TestMethod()]
        public void WebExceptionConstructorMessageTest() {
            string expected = "Exception Message";
            WebException target = new WebException(expected);
            Assert.IsNotNull(target);

            string actual = target.Message;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WebException Constructor
        ///</summary>
        [TestMethod()]
        public void WebExceptionConstructorMessageInnerExceptionTest() {
            string expectedMessage = "Exception Message";
            Exception expectedInnerException = new NotImplementedException();
            WebException target = new WebException(expectedMessage, expectedInnerException);
            Assert.IsNotNull(target);

            string actualMessage = target.Message;
            Assert.AreEqual(expectedMessage, actualMessage);

            Exception actualInnerException = target.InnerException;
            Assert.AreEqual(expectedInnerException, actualInnerException);
        }

        /// <summary>
        ///A test for WebException Constructor
        ///</summary>
        [TestMethod()]
        public void WebExceptionConstructorSerializationTest() {
            WebException source, target;
            source = new WebException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (WebException)formatter.Deserialize(formatStream);
            }

            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetObjectData
        ///</summary>
        [TestMethod()]
        public void WebExceptionGetObjectDataTest() {
            WebException source, target;
            source = new WebException();

            //FUTUREDEV: Set any custom data properties and verify values after deserialization

            using (Stream formatStream = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(formatStream, source);
                formatStream.Position = 0; //NOTE:  Reset stream

                target = (WebException)formatter.Deserialize(formatStream); //NOTE:  This will cause a call to GetObjectData
            }

            Assert.IsNotNull(target);
        }
    }
}