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
        public readonly T1 Value1;
        public readonly T2 Value2;

        public Pair(T1 value1, T2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public KeyValuePair<T1,T2> ToKeyValuePair()
        {
            return new KeyValuePair<T1, T2>(Value1, Value2);
        }

        public KeyValuePair<TK,TV> ToKeyValuePair<TK, TV>(Func<Pair<T1,T2>,TK> KeySelector, Func<Pair<T1,T2>,TV> ValueSelector)
        {
            return new KeyValuePair<TK, TV>(KeySelector(this), ValueSelector(this));
        }
    }
    
    public static class Extensions
    {

        public static void ForEach<T>(this IEnumerable<T> e, Action<T> Action)
        {
            foreach(T item in e)
                Action(item);
        }


        public static IEnumerable<Pair<T1,T2>> Conbinate<T1, T2>(this IEnumerable<T1> e, IEnumerable<T2> Target)
        {
            return
                from x in e
                from y in Target
                select new Pair<T1,T2>(x, y);
        }
    }
}

