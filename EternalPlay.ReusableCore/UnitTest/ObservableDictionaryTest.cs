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

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using EternalPlay.ReusableCore.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EternalPlay.ReusableCore.UnitTest {
    /// <summary>
    ///This is a test class for ObservableDictionaryTest and is intended
    ///to contain all ObservableDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObservableDictionaryTest {


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
        ///A test for the empty ObservableDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryConstructorEmptyTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target= new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for the ObservableDictionary Constructor that wraps an existing IDictionary.
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryConstructorSourceDictionaryTest() {
            IDictionary<GenericParameterHelper, GenericParameterHelper> source = new Dictionary<GenericParameterHelper, GenericParameterHelper>();
            source.Add(new GenericParameterHelper(1), new GenericParameterHelper(1));
            source.Add(new GenericParameterHelper(2), new GenericParameterHelper(2));

            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target= new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>(source);
            Assert.IsNotNull(target);
            Assert.AreEqual<int>(2, target.Count);
        }

        /// <summary>
        ///A test for Adding items by key and value
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryAddKeyValueTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
                Assert.IsNotNull(e.NewItems);
            };

            target.Add(new GenericParameterHelper(1), new GenericParameterHelper(1));
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for Adding single key value pair object items
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryAddItemTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
                Assert.IsNotNull(e.NewItems);
            };

            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            target.Add(item);
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryClearTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            target.Add(new GenericParameterHelper(1), new GenericParameterHelper(1));
            target.Add(new GenericParameterHelper(2), new GenericParameterHelper(2));

            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Reset, e.Action);
            };

            target.Clear();
            Assert.AreEqual(0, target.Count);
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryContainsTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNotAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(itemAdded);

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
        public void ObservableDictionaryContainsKeyTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNotAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(itemAdded);

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
        public void ObservableDictionaryCopyToTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(item0);
            target.Add(item1);

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
        public void ObservableDictionaryGetEnumeratorTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(item0);
            target.Add(item1);

            IEnumerator<KeyValuePair<GenericParameterHelper, GenericParameterHelper>> actual;
            actual = target.GetEnumerator();

            Assert.IsNotNull(actual);

            actual.MoveNext();
            Assert.AreEqual(item0, actual.Current);
            actual.MoveNext();
            Assert.AreEqual(item1, actual.Current);
        }

        /// <summary>
        ///A test for OnCollectionChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void ObservableDictionaryOnCollectionChangedTest() {
            ObservableDictionary_Accessor<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary_Accessor<GenericParameterHelper, GenericParameterHelper>();

            target.add_CollectionChanged(delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Reset, e.Action);
            });

            target.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryRemoveItemTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNonAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(itemAdded);
            Assert.AreEqual(1, target.Count);

            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.IsNotNull(e.OldItems);
            };

            bool expectedAdded = true;
            bool expectedNotAdded = false;
            bool actualAdded = target.Remove(itemAdded);
            bool actualNotAdded = target.Remove(itemNonAdded);

            Assert.AreEqual(expectedAdded, actualAdded);
            Assert.AreEqual(expectedNotAdded, actualNotAdded);

            Assert.AreEqual(0, target.Count);
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryRemoveKeyTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNonAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(itemAdded);
            Assert.AreEqual(1, target.Count);

            target.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e) {
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.IsNotNull(e.OldItems);
            };

            bool expectedAdded = true;
            bool expectedNotAdded = false;
            bool actualAdded = target.Remove(new GenericParameterHelper(1));
            bool actualNotAdded = target.Remove(new GenericParameterHelper(2));

            Assert.AreEqual(expectedAdded, actualAdded);
            Assert.AreEqual(expectedNotAdded, actualNotAdded);

            Assert.AreEqual(0, target.Count);
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EternalPlay.ReusableCore.dll")]
        public void ObservableDictionaryGetNonGenericEnumeratorTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> dictionary = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            IEnumerable target = dictionary;
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item0 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item1 = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            dictionary.Add(item0);
            dictionary.Add(item1);

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
        public void ObservableDictionaryTryGetValueTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> itemNonAdded = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(2), new GenericParameterHelper(2));
            target.Add(itemAdded);

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
        public void ObservableDictionaryCountTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));

            Assert.AreEqual(0, target.Count);
            target.Add(item);
            Assert.AreEqual(1, target.Count);
        }

        /// <summary>
        ///A test for IsReadOnly
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryIsReadOnlyTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> mutableTarget = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper> readOnlySource = new ReadOnlyDictionary<GenericParameterHelper, GenericParameterHelper>(new Dictionary<GenericParameterHelper, GenericParameterHelper>());
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> readOnlyTarget = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>(readOnlySource);

            Assert.IsFalse(mutableTarget.IsReadOnly);
            Assert.IsTrue(readOnlyTarget.IsReadOnly);
        }

        /// <summary>
        ///A test for Item (Indexer)
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryItemTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            GenericParameterHelper itemKey = new GenericParameterHelper(1);
            GenericParameterHelper itemValue = new GenericParameterHelper(1);

            Assert.AreEqual(0, target.Count);

            target[itemKey] = itemValue;
            Assert.AreEqual(1, target.Count);

            GenericParameterHelper expected, actual;
            expected = itemValue;
            actual = target[itemKey];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Values
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryValuesTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            target.Add(item);

            Assert.IsNotNull(target.Keys);
            Assert.AreEqual(1, target.Keys.Count);
        }

        /// <summary>
        ///A test for Keys
        ///</summary>
        [TestMethod()]
        public void ObservableDictionaryKeysTest() {
            ObservableDictionary<GenericParameterHelper, GenericParameterHelper> target = new ObservableDictionary<GenericParameterHelper, GenericParameterHelper>();
            KeyValuePair<GenericParameterHelper, GenericParameterHelper> item = new KeyValuePair<GenericParameterHelper, GenericParameterHelper>(new GenericParameterHelper(1), new GenericParameterHelper(1));
            target.Add(item);

            Assert.IsNotNull(target.Values);
            Assert.AreEqual(1, target.Values.Count);
        }
    }
}