using System;
using System.Data.Linq;
using System.Collections.Generic;

namespace CoreTweet
{
    public abstract class CoreBase
    {
        /// <summary>
        ///     この子を呼べばTに対応するConvert()を呼んでdynamic objectをstatic objectに変換してくれます
        /// </summary>
        internal static T Convert<T>(dynamic e)
            where T : CoreBase
        {
            var i = Activator.CreateInstance<T>();
            i.ConvertBase(e);
            return i;
        }
        
        /// <summary>
        ///     ( ,,Ծ ‸ Ծ ).｡ｏO( 説明いるのかな )
        /// </summary>
        internal static T[] ConvertArray<T>(dynamic e)
			where T : CoreBase
        {
            var l = new List<T>();
            foreach(dynamic x in e) {
                l.Add(Convert<T>(x));
            }
            return l.ToArray();
        }

        /// <summary>
        ///     この子をそれぞれのクラスに実装して具体的な変換を行います
        /// </summary>
        internal abstract void ConvertBase(dynamic e);
    }
}