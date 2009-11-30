using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EternalPlay.ReusableCore.Extensions {
    /// <summary>
    /// Extenion methods class for IEnumerable(T)
    /// </summary>
    public static class IEnumerableExtensions {
        /// <summary>
        /// Executes the given action on each item in the source enumeration
        /// </summary>
        /// <remarks>
        /// Similar to the ForEach methods on List(T) and Array, allows easy lamda expression anonymous method execution on all the items
        /// in an enumerable collection.
        /// </remarks>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">Collection of items to invoke the action on</param>
        /// <param name="action">Function to apply to each element</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            foreach (TSource item in source)
                action(item);
        }
    }
}