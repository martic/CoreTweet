using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Help
        {
            //DONE!
            
            //GET Methods
            
            /// <summary>
            /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
            /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
            /// </summary>
            /// <para>Avaliable parameters: Nothing</para><para> </para>
            /// <returns>Configurations.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Configurations Configuration(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Configurations>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("help/configuation"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code. The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
            /// </summary>
            /// <para>Avaliable parameters: Nothing.</para><para> </para>
            /// <returns>Languages.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Language> Languages(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Language>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("help/languages"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns Twitter's Privacy Policy.</para>
            /// </summary>
            /// <para>Avaliable parameters: Nothing.</para><para> </para>
            /// <returns>The sentense.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static string Privacy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("help/privacy"), Parameters)).privacy;
            }
            
            /// <summary>
            /// <para>Returns the Twitter Terms of Service in the requested format. These are not the same as the Developer Rules of the Road.</para>
            /// </summary>
            /// <para>Avaliable parameters: Nothing.</para><para> </para>
            /// <returns>The sentense.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static string Tos(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("help/tos"), Parameters)).tos;
            }
            
        }
    }
}

