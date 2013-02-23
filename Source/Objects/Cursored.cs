using System;
using System.Linq;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Cursored<T> : CoreBase, IEnumerable<T>
    {
        public T[] Result{ get; set; }

        public long NextCursor{ get; set; }

        public long PreviousCursor{ get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Result = ParamByType<T>(e);
            NextCursor = (long)e.next_cursor;
            PreviousCursor = (long)e.previous_cursor;
        }

        internal static T2[] ParamByType<T2>(dynamic e)
        {
            if(typeof(T2) == typeof(long))
                return e.ids as T2[];
            else if(typeof(T2) == typeof(User))
                return CoreBase.ConvertArray<User>(e.users) as T2[];
            else
                throw new InvalidOperationException("This type can't be cursored.");
        }
        
        public System.Collections.IEnumerator GetEnumerator()
        {
            return Result.GetEnumerator();
        }
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (Result as IEnumerable<T>).GetEnumerator();
        }

    }
}

