using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Alice
{
    /// <summary>
    /// Alice's extension methods.
    /// </summary>
    public static class Extensions
    {
    
        public static IEnumerable<string> EnumerateLines(this StreamReader streamReader)
        {
            while(!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }

        /// <summary>
        /// Performs the specified action on each element on the enumerable object.
        /// </summary>
        /// <param name="e">The enumerable object.</param>
        /// <param name="Action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> e, Action<T> action)
        {
            foreach(T item in e)
                action(item);
        }

        /// <summary>
        /// Gets all combinations of this and the specified target.
        /// </summary>
        /// <example>
        /// new []{0,1,2}.Conbinate(new []{"a","b"}) -> {0,"a"},{0,"b"},{1,"a"},{1,"b"},{2,"a"},{2,"b"}
        /// </example>
        /// <param name="e">The enumerable object.</param>
        /// <param name="Target">The enumerable object.</param>
        /// <typeparam name="T1">The 1st type parameter.</typeparam>
        /// <typeparam name="T2">The 2nd type parameter.</typeparam>
        /// <returns>Tuples of conbinated objects.</returns>
        public static IEnumerable<Tuple<T1,T2>> Conbinate<T1, T2>(this IEnumerable<T1> e, IEnumerable<T2> target)
        {
            return
                from x in e
                from y in target
                select Tuple.Create(x, y);
        }

		/// <summary>
		/// Converts camelCase text to snake_case.
		/// </summary>
		/// <returns>The snake_case text.</returns>
		/// <param name="e">The camelCase text.</param>
		public static string ToSnakeCase(this string e)
		{
			return string.Concat(e.Select(x => char.IsUpper(x) ? "_" + x.ToString().ToLower() : x.ToString()));
		}

    }
}

