using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace EternalPlay.ReusableCore.Collections {
    /// <summary>
    /// Represents a dynamic key value pair collection that provides notifications when items are added, removed or the entire
    /// collection is cleared.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary</typeparam>
    public sealed class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged {
        #region Private Fields
        private IDictionary<TKey, TValue> _dictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new empty dictionary.
        /// </summary>
        public ObservableDictionary() {
            //NOTE:  For default construction, use a new generic dictionary
            _dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Constructs a new observable dictionary wrapper around the existing source dictionary
        /// </summary>
        /// <remarks>
        /// This wrapper provides and manages all notifications.  Changes made directly to the source will not cause events
        /// to be fired, even after being wrapped in an ObservableDictionary.  All changes should be made to the observable
        /// dictionary object directly, not to the original source dictionary.
        /// </remarks>
        /// <param name="sourceDictionary">Source generic dictionary to provide change notifications for.</param>
        public ObservableDictionary(IDictionary<TKey, TValue> sourceDictionary) {
            _dictionary = sourceDictionary;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Internal function to fire the CollectionChanged event
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (CollectionChanged != null)
                CollectionChanged(sender, e);
        }
        #endregion

        #region Interface Implementations
        #region INotifyCollectionChanged Members

        /// <summary>
        /// Occurs when the collection changes
        /// </summary>
        /// <remarks>
        /// The event handler receives an argument of type NotifyCollectionChangedEventArgs,which contains data that is related to this event
        /// </remarks>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region IDictionary<TKey,TValue> Members
        /// <summary>
        /// Adds an element with the provided key and value to the IDictionary{TKey, TValue}).
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value) {
            _dictionary.Add(key, value);
            this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
        }

        /// <summary>
        /// Determines whether the IDictionary{TKey, TValue}) contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the IDictionary{TKey, TValue}).</param>
        /// <returns>true if the IDictionary{TKey, TValue}) contains an element with the key; otherwise, false.</returns>
        public bool ContainsKey(TKey key) {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets an ICollection{T}) containing the keys of the IDictionary{TKey, TValue}).
        /// </summary>
        /// <remarks>
        /// The order of the keys in the returned ICollection{T}) is unspecified, but it is guaranteed to be the same order as the corresponding values in the ICollection{T}) returned by the Values property.
        /// </remarks>
        public ICollection<TKey> Keys {
            get {
                return _dictionary.Keys;
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the IDictionary{TKey, TValue}).
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the original IDictionary{TKey, TValue}).</returns>
        public bool Remove(TKey key) {
            if (_dictionary.ContainsKey(key)) {
                KeyValuePair<TKey, TValue> removedItem = new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
                bool success = _dictionary.Remove(key);
                this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem));
                return success;
            } else
                return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the object that implements IDictionary{TKey, TValue}) contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value) {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets an ICollection{T}) containing the values in the IDictionary{TKey, TValue}).
        /// </summary>
        /// <remarks>
        /// The order of the values in the returned ICollection{T}) is unspecified, but it is guaranteed to be the same order as the corresponding keys in the ICollection{T}) returned by the Keys property.
        /// </remarks>
        public ICollection<TValue> Values {
            get {
                return _dictionary.Values;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        public TValue this[TKey key] {
            get {
                return _dictionary[key];
            }
            set {
                KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(key, value);
                _dictionary[key] = value;
                this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem));
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members
        /// <summary>
        /// Adds an item to the ICollection{T})
        /// </summary>
        /// <param name="item">The object to add to the ICollection{T})</param>
        public void Add(KeyValuePair<TKey, TValue> item) {
            _dictionary.Add(item);
            this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// Removes all items from the ICollection{T}).
        /// </summary>
        public void Clear() {
            _dictionary.Clear();
            this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Determines whether the ICollection{T}) contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the ICollection{T}).</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item) {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the ICollection{T}) to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection{T}). The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            _dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the ICollection{T}).
        /// </summary>
        public int Count {
            get {
                return _dictionary.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the ICollection{T}) is read-only.
        /// </summary>
        public bool IsReadOnly {
            get {
                return _dictionary.IsReadOnly;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the ICollection{T}).
        /// </summary>
        /// <param name="item">The object to remove from the ICollection{T}).</param>
        /// <returns>true if item was successfully removed from the ICollection{T}); otherwise, false. This method also returns false if item is not found in the original ICollection{T}).</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item) {
            if (_dictionary.ContainsKey(item.Key)) {
                bool success = _dictionary.Remove(item);
                this.OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                return success;
            } else
                return false;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A IEnumerator{T}) that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return _dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        #endregion
        #endregion
    }
}