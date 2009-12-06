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
using System.Collections;
using System.Collections.Generic;
using EternalPlay.ReusableCore.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EternalPlay.ReusableCore.UnitTest {

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
