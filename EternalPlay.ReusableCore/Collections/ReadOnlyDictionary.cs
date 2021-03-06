﻿#region License
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

namespace EternalPlay.ReusableCore.Collections {
    /// <summary>
    /// Provides a read only key value pair collection.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary</typeparam>
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue> {
        #region Private Fields
        private IDictionary<TKey, TValue> _dictionary;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new readonly dictionary wrapper around the existing source dictionary
        /// </summary>
        /// <remarks>
        /// This wrapper provides and manages access to the interal dictionary.  Any attempt to modify the contents of
        /// the dictionary result in a System.NotSupported exception.
        /// </remarks>
        /// <param name="sourceDictionary">Source generic dictionary to provide read only access for.</param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> sourceDictionary) {
            _dictionary = sourceDictionary;
        }
        #endregion

        #region Interface Implementations
        #region IDictionary<TKey,TValue> Members
        /// <summary>
        /// Adds an element with the provided key and value to the IDictionary{TKey, TValue}).
        /// </summary>
        /// <remarks>
        /// Any call to this method will result in a not supported exception.
        /// </remarks>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(TKey key, TValue value) {
            throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
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
        /// <remarks>
        /// Any call to this method will result in a not supported exception.
        /// </remarks>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the original IDictionary{TKey, TValue}).</returns>
        public bool Remove(TKey key) {
            throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
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
        /// <remarks>
        /// Any attempts to set values will result in a not supported exception.
        /// </remarks>
        /// <param name="key">The key of the element to get or set.</param>
        public TValue this[TKey key] {
            get {
                return _dictionary[key];
            }

            set {
                throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members
        /// <summary>
        /// Adds an item to the ICollection{T})
        /// </summary>
        /// <remarks>
        /// Any call to this method will result in a not supported exception.
        /// </remarks>
        /// <param name="item">The object to add to the ICollection{T})</param>
        public void Add(KeyValuePair<TKey, TValue> item) {
            throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
        }

        /// <summary>
        /// Removes all items from the ICollection{T}).
        /// </summary>
        /// <remarks>
        /// Any call to this method will result in a not supported exception.
        /// </remarks>
        public void Clear() {
            throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
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
        /// <remarks>
        /// For a ReadOnlyDictionary this property always returns true.
        /// </remarks>
        public bool IsReadOnly {
            get {
                return true;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the ICollection{T}).
        /// </summary>
        /// <param name="item">The object to remove from the ICollection{T}).</param>
        /// <remarks>
        /// Any call to this method will result in a not supported exception.
        /// </remarks>
        /// <returns>true if item was successfully removed from the ICollection{T}); otherwise, false. This method also returns false if item is not found in the original ICollection{T}).</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item) {
            throw new NotSupportedException("ReadOnlyDictionary contents cannot be modified.");
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
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        #endregion
        #endregion
    }
}
