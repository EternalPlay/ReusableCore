using EternalPlay.ReusableCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace EternalPlay.ReusableCore.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for IEnumerableExtensionsTest and is intended
    ///to contain all IEnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IEnumerableExtensionsTest {


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
        /// Tests the ForEach extenion method to the <see cref="IEnumerable{T}" /> interface.
        /// </summary>
        [TestMethod()]
        public void IEnumerableExtensionsForEachTest() {
            List<int> source = new List<int>();
            IEnumerable<int> target = source;

            for (int i = 0, iC = 5; i < iC; i++)
                source.Add(1);

            int actualCount, expectedCount;
            actualCount = 0;
            expectedCount = 5;

            target.ForEach(i => {
                actualCount++;   //NOTE:  increment parent counter for verifying execution
            });

            Assert.AreEqual(expectedCount, actualCount, "Results of action execution did not match expectations.");
        }
    }
}
