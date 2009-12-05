using EternalPlay.ReusableCore.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Collections;

namespace EternalPlay.ReusableCore.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for ReadOnlyDictionaryTest and is intended
    ///to contain all ReadOnlyDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReadOnlyDictionaryTest {


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
        ///A test for ReadOnlyDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryConstructorTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Add using a seperate key and value
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryAddKeyValueTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            try {
                target.Add(new GenericParameterHelper(1), new GenericParameterHelper(1));
                Assert.Fail("Adding an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }
        }

        /// <summary>
        ///A test for Add using a KeyValuePair item
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryAddItemTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            try {
                target.Add(new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1)));
                Assert.Fail("Adding an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryClearTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            try {
                target.Clear();
                Assert.Fail("Adding an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryContainsTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNotAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(itemAdded);
            
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            bool expectedAdded = true;
            bool expectedNotAdded = false;
            bool actualAdded = target.Contains(itemAdded);
            bool actualNotAdded = target.Contains(itemNotAdded);

            Assert.AreEqual(expectedAdded, actualAdded);
            Assert.AreEqual(expectedNotAdded, actualNotAdded);
        }

        /// <summary>
        ///A test for ContainsKey
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryContainsKeyTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNotAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(itemAdded);

            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            bool expectedAdded = true;
            bool expectedNotAdded = false;
            bool actualAdded = target.ContainsKey(new GenericParameterHelper(1));
            bool actualNotAdded = target.ContainsKey(new GenericParameterHelper(2));

            Assert.AreEqual(expectedAdded, actualAdded);
            Assert.AreEqual(expectedNotAdded, actualNotAdded);
        }

        /// <summary>
        ///A test for CopyTo
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryCopyToTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(item0);
            source.Add(item1);

            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            KeyValuePair<GenericParameterHelper, GenericParameterHelper>[] array = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>[2];
            int arrayIndex = 0;
            target.CopyTo(array, arrayIndex);

            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(item0, array[0]);
            Assert.AreEqual(item1, array[1]);
        }

        /// <summary>
        ///A test for GetEnumerator
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryGetEnumeratorTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(item0);
            source.Add(item1);

            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            IEnumerator<KeyValuePair<GenericParameterHelper, GenericParameterHelper>> actual;
            actual = target.GetEnumerator();

            Assert.IsNotNull(actual);

            actual.MoveNext();
            Assert.AreEqual(item0, actual.Current);
            actual.MoveNext();
            Assert.AreEqual(item1, actual.Current);
        }

        /// <summary>
        ///A test for Remove using a key
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryRemoveKeyTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            source.Add(new GenericParameterHelper(1), new GenericParameterHelper(1));
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            try {
                target.Remove(new GenericParameterHelper(1));
                Assert.Fail("Removing an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }
        }

        /// <summary>
        ///A test for Remove using an item
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryRemoveItemTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            source.Add(item);
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            try {
                target.Remove(item);
                Assert.Fail("Removing an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void ReadOnlyDictionaryGetNonGenericEnumeratorTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(item0);
            source.Add(item1);
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> readOnlySource = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            IEnumerable target = readOnlySource;
            IEnumerator actual;
            actual = target.GetEnumerator();

            Assert.IsNotNull(actual);

            actual.MoveNext();
            Assert.AreEqual(item0, actual.Current);
            actual.MoveNext();
            Assert.AreEqual(item1, actual.Current);
        }

        /// <summary>
        ///A test for TryGetValue
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryTryGetValueTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNonAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            source.Add(itemAdded);

            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            GenericParameterHelper expectedItemAdded, expectidItemNonAdded;
            bool expectedAdded = true;
            bool expectedNotAdded = false;
            bool actualAdded = target.TryGetValue(new GenericParameterHelper(1), out expectedItemAdded);
            bool actualNotAdded = target.TryGetValue(new GenericParameterHelper(2), out expectidItemNonAdded);

            Assert.AreEqual(expectedAdded, actualAdded);
            Assert.AreEqual(expectedNotAdded, actualNotAdded);
            Assert.AreEqual(expectedItemAdded, itemAdded.Value);
            Assert.IsNull(expectidItemNonAdded);
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryCountTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            Assert.AreEqual(0, target.Count);
            source.Add(item);
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for IsReadOnly
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryIsReadOnlyTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            Assert.IsTrue(target.IsReadOnly);
        }

        /// <summary>
        ///A test for Item (Indexer)
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryItemTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            GenericParameterHelper itemKey = new GenericParameterHelper(1);
            GenericParameterHelper itemValue = new GenericParameterHelper(1);
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);
            
            Assert.AreEqual(0, target.Count);

            try {
                target[itemKey] = itemValue;
                Assert.Fail("Adding an item should have thrown an error.");
            } catch (Exception e) {
                Assert.IsInstanceOfType(e, typeof(NotSupportedException), "An unknown exception type was thrown.");
            }

            source.Add(itemKey, itemValue);

            GenericParameterHelper expected, actual;
            expected = itemValue;
            actual = target[itemKey];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Values
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryValuesTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            source.Add(item);
            
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            Assert.IsNotNull(target.Keys);
            Assert.AreEqual(1, target.Keys.Count);
        }

        /// <summary>
        ///A test for Keys
        ///</summary>
        [TestMethod()]
        public void ReadOnlyDictionaryKeysTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            source.Add(item);

            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> target = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(source);

            Assert.IsNotNull(target.Values);
            Assert.AreEqual(1, target.Values.Count);
        }
    }
}
