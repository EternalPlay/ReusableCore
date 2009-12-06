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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using EternalPlay.ReusableCore.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EternalPlay.ReusableCore.Web.UnitTest {

    /// <summary>
    ///This is a test class for UrlBuilderTest and is intended
    ///to contain all UrlBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UrlBuilderTest {


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
        ///A test for UrlBuilder Constructor
        ///</summary>
        [TestMethod()]
        public void UrlBuilderConstructorEmptyTest() {
            UrlBuilder target = new UrlBuilder();

            //NOTE:  Verify non null construction
            Assert.IsNotNull(target);

            Assert.IsTrue(string.IsNullOrEmpty(target.Query));
            Assert.IsNotNull(target.QueryString);
            Assert.AreEqual(0, target.QueryString.Count);
        }



        /// <summary>
        ///A test for UrlBuilder Constructor
        ///</summary>
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification = "Testing the string constructor.")]
        [TestMethod()]
        public void UrlBuilderConstructorStringTest() {
            string uri = "http://test/page?queryKey=queryValue";

            UrlBuilder target = new UrlBuilder(uri);

            Assert.IsNotNull(target);
            Assert.AreEqual("?queryKey=queryValue", target.Query);
            Assert.IsNotNull(target.QueryString);
            Assert.AreEqual(1, target.QueryString.Count);
            Assert.AreEqual("page", target.PageName);
        }

        /// <summary>
        ///A test for UrlBuilder Constructor
        ///</summary>
        [TestMethod()]
        public void UrlBuilderConstructorUriTest() {
            Uri uri = new Uri("http://test/page?queryKey=queryValue");
            UrlBuilder target = new UrlBuilder(uri);

            Assert.IsNotNull(target);
            Assert.AreEqual("?queryKey=queryValue", target.Query);
            Assert.IsNotNull(target.QueryString);
            Assert.AreEqual(1, target.QueryString.Count);
            Assert.AreEqual("page", target.PageName);
        }

        /// <summary>
        ///A test for InitializeQueryString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.Web.dll")]
        public void UrlBuilderInitializeQueryStringTest() {
            UrlBuilder_Accessor target;

            //NOTE:  Test with empty constructor
            target = new UrlBuilder_Accessor();
            target.InitializeQueryString();

            Assert.IsNotNull(target);
            Assert.IsNotNull(target._internalQueryString);
            Assert.IsNotNull(target._queryString);
            Assert.AreEqual(0, target.QueryString.Count);

            //NOTE:  Test with URL string constructor
            Uri uri = new Uri("http://test/page?queryKey=queryValue");
            target = new UrlBuilder_Accessor(uri);

            Assert.IsNotNull(target);
            Assert.IsNotNull(target._internalQueryString);
            Assert.IsNotNull(target._queryString);
            Assert.AreEqual(1, target.QueryString.Count);
        }

        /// <summary>
        ///A test for QueryString_CollectionChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.Web.dll")]
        public void UrlBuilderQueryStringCollectionChangedTest() {
            UrlBuilder_Accessor target = new UrlBuilder_Accessor(); // TODO: Initialize to an appropriate value

            //NOTE:  Ensure creation with an empty query string
            Assert.IsNotNull(target);
            Assert.IsTrue(string.IsNullOrEmpty(target._query));

            //NOTE:  Directly set a new internal dictionary value, call the handler and ensure query string is updated
            target._internalQueryString.Add("testKey", "testValue");
            object sender = null;
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            target.QueryString_CollectionChanged(sender, e);

            Assert.AreEqual("?testKey=testValue", target._query);
        }

        /// <summary>
        ///A test for SynchronizeQueryString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.Web.dll")]
        public void UrlBuilderSynchronizeQueryStringTest() {
            UrlBuilder_Accessor target = new UrlBuilder_Accessor();

            Assert.IsNotNull(target);
            Assert.AreEqual(0, target.QueryString.Count);

            target._query = "?testKey=testValue";
            target.SynchronizeQueryString();

            Assert.AreEqual(1, target.QueryString.Count);
            Assert.AreEqual("testValue", target.QueryString["testKey"]);
        }

        /// <summary>
        ///A test for SynchronizeQuery
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.Web.dll")]
        public void UrlBuilderSynchronizeQueryTest() {
            UrlBuilder_Accessor target = new UrlBuilder_Accessor();

            Assert.IsNotNull(target);

            target.SynchronizeQuery();
            Assert.IsTrue(string.IsNullOrEmpty(target._query));

            target._internalQueryString.Add("testKey", "testValue");
            target.SynchronizeQuery();

            Assert.AreEqual("?testKey=testValue", target._query);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void UrlBuilderToStringTest() {
            UrlBuilder target = new UrlBuilder(); // TODO: Initialize to an appropriate value
            target.Scheme = "http";
            target.Host = "test";
            target.Path = "/";
            target.PageName = "test";
            target.Query = "testKey=testValue";

            string actual, expected;
            expected = "http://test/test?testKey=testValue";
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PageName
        ///</summary>
        [TestMethod()]
        public void UrlBuilderPageNameTest() {
            UrlBuilder target = new UrlBuilder();
            string expected = "TestPage";
            string actual;
            target.PageName = expected;
            actual = target.PageName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for QueryString
        ///</summary>
        [TestMethod()]
        public void UrlBuilderQueryStringTest() {
            UrlBuilder_Accessor target = new UrlBuilder_Accessor();

            target._internalQueryString = new Dictionary<string, object>();
            IDictionary<string, object> expected = target._queryString = new ObservableDictionary<string, object>(target._internalQueryString);
            IDictionary<string, object> actual = target.QueryString;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Query
        ///</summary>
        [TestMethod()]
        public void UrlBuilderQueryTest() {
            UrlBuilder target = new UrlBuilder();
            string expected = "?testKey=testValue";
            string actual;
            target.Query = expected;
            actual = target.Query;
            Assert.AreEqual(expected, actual);
        }
    }
}