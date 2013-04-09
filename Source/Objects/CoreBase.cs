using System;
using System.Linq;
using System.Collections.Generic;

namespace CoreTweet.Core
{
    public abstract class CoreBase : TokenIncluded
    {
        internal CoreBase(Tokens tokens) : base(tokens) { }
        
        /// <summary>
        ///     この子を呼べばTに対応するConvert()を呼んでdynamic objectをstatic objectに変換してくれます
        /// </summary>
        public static T Convert<T>(Tokens tokens, dynamic e)
            where T : CoreBase
        {
            var i = Activator.CreateInstance(typeof(T), tokens) as T;
            i.ConvertBase(e);
            return i;
        }
        
        /// <summary>
        ///     ( ,,Ծ ‸ Ծ ).｡ｏO( 説明いるのかな )
        /// </summary>
        public static IEnumerable<T> ConvertArray<T>(Tokens tokens, dynamic e)
            where T : CoreBase
        {
            if(e == null || !e.IsArray)
                return null;
            T[] ts = new T[((dynamic[])e).Length];
            for(int i = 0; i < ((dynamic[])e).Length; i++)
                ts[i] = Convert<T>(tokens, ((dynamic[])e)[i]);
            return ts;
        }

        /// <summary>
        ///     この子をそれぞれのクラスに実装して具体的な変換を行います
        /// </summary>
        internal abstract void ConvertBase(dynamic e);
    }
}