using System;
using System.Linq;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Cursored<T> : CoreBase, IEnumerable<T>
    {
        public IEnumerable<T> Result{ get; set; }

        public long NextCursor{ get; set; }

        public long PreviousCursor{ get; set; }
        
        public Cursored(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Result = ParamByType<T>(this.Tokens, e);
            NextCursor = (long)e.next_cursor;
            PreviousCursor = (long)e.previous_cursor;
        }

        public static T2[] ParamByType<T2>(Tokens tokens, dynamic e)
        {
            if(typeof(T2) == typeof(long))
                return e.ids;
            else if(typeof(T2) == typeof(User))
                return CoreBase.ConvertArray<User>(tokens, e.users) ;
            else if(typeof(T2) == typeof(CoreTweet.List))
                return CoreBase.ConvertArray<CoreTweet.List>(tokens, e.lists);
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

