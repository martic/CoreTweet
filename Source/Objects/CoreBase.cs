using System;
using System.Linq;
using System.Collections.Generic;

namespace CoreTweet.Core
{
    public abstract class CoreBase : TokenIncluded
    {
        public CoreBase() : base() { }
        
        public CoreBase(Tokens tokens) : base(tokens) { }
        
        /// <summary>
        /// Convert dynamic object to specified type.
        /// </summary>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='e'>
        /// Dynamic object.
        /// </param>
        /// <typeparam name='T'>
        /// The 1st type parameter.
        /// </typeparam>
        public static T Convert<T>(Tokens tokens, dynamic e)
            where T : CoreBase
        {
            var i = (T)typeof(T).InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, new []{tokens});
            i.ConvertBase(e);
            return i;
        }
        
        /// <summary>
        /// Convert dynamic object to an array of specified type.
        /// </summary>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='e'>
        /// Dynamic object.
        /// </param>
        /// <typeparam name='T'>
        /// The 1st type parameter.
        /// </typeparam>
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
        /// Implementation for CoreBase.Convert.
        /// </summary>
        internal abstract void ConvertBase(dynamic e);
    }
    
    /// <summary>
    /// The token included class.
    /// </summary>
    public abstract class TokenIncluded
    {
        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>
        /// The tokens.
        /// </value>
        protected internal Tokens Tokens { get; set; }

		public Tokens IncludedTokens
		{
			get
			{
				return this.Tokens;
			}
		}
        
        public TokenIncluded() : this(null) { }
        
        public TokenIncluded(Tokens tokens)
        {
            Tokens = tokens;
        }
    }
}