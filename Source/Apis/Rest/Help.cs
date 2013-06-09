using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet.Rest
{

    /// <summary>GET help</summary>
    public class Help : TokenIncluded
    {
        internal Help(Tokens e) : base(e) { }
        //DONE!
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>Configurations.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Configurations Configuration(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Configurations>(MethodType.Get, "help/configuation", parameters);
        }
            
        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code. The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>Languages.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Language> Languages(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Language>(MethodType.Get, "help/languages", parameters);
        }
            
        /// <summary>
        /// <para>Returns Twitter's Privacy Policy.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>The sentense.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public string Privacy(params Expression<Func<string,object>>[] parameters)
        {
            return (string)DynamicJson.Parse(
                    this.Tokens.SendRequest(MethodType.Get, "help/privacy", parameters)).privacy;
        }
            
        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format. These are not the same as the Developer Rules of the Road.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>The sentense.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public string Tos(params Expression<Func<string,object>>[] parameters)
        {
            return (string)DynamicJson.Parse(
                    this.Tokens.SendRequest(MethodType.Get, "help/tos", parameters)).tos;
        }
            
    }
}
