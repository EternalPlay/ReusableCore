using EternalPlay.ReusableCore.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.UI;
using System.Collections.Specialized;
using System.Collections.Generic;
using EternalPlay.ReusableCore.Collections;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings", Justification="Testing the string constructor.")]
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