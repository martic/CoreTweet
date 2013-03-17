using System;
using System.Linq;
using System.Collections.Generic;

namespace Alice
{
    /// <summary>
    /// A pair of values.
    /// </summary>
    public struct Pair<T1,T2>
    {
        /// <summary>
        /// The first value.
        /// </summary>
        public readonly T1 Value1;
        /// <summary>
        /// The second value.
        /// </summary>
        public readonly T2 Value2;

        /// <summary>
        /// Initializes a new instance of the Alice.Pair struct.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        public Pair(T1 value1, T2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        /// <summary>
        /// Converts this instance to KeyValuePair.
        /// </summary>
        /// <returns>The key value pair.</returns>
        public KeyValuePair<T1,T2> ToKeyValuePair()
        {
            return new KeyValuePair<T1, T2>(Value1, Value2);
        }

        /// <summary>
        /// Converts this instance to KeyValuePair.
        /// </summary>
        /// <returns>The key value pair.</returns>
        /// <param name="KeySelector">Key selector.</param>
        /// <param name="ValueSelector">Value selector.</param>
        /// <typeparam name="TK">The 1st type parameter.</typeparam>
        /// <typeparam name="TV">The 2nd type parameter.</typeparam>
        public KeyValuePair<TK,TV> ToKeyValuePair<TK, TV>(Func<Pair<T1,T2>,TK> KeySelector, Func<Pair<T1,T2>,TV> ValueSelector)
        {
            return new KeyValuePair<TK, TV>(KeySelector(this), ValueSelector(this));
        }
    }

    /// <summary>
    /// Alice's extension methods.
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Performs the specified action on each element on the enumerable object.
        /// </summary>
        /// <param name="e">The enumerable object.</param>
        /// <param name="Action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> Action)
        {
            foreach(T item in e)
                Action(item);
        }

        /// <summary>
        /// Gets all combinations of this and specified target.
        /// </summary>
        /// <param name="e">The enumerable object.</param>
        /// <param name="Target">The enumerable object.</param>
        /// <typeparam name="T1">The 1st type parameter.</typeparam>
        /// <typeparam name="T2">The 2nd type parameter.</typeparam>
        public static IEnumerable<Pair<T1,T2>> Conbinate<T1, T2>(this IEnumerable<T1> e, IEnumerable<T2> Target)
        {
            return
                from x in e
                from y in Target
                select new Pair<T1,T2>(x, y);
        }
    }
}

