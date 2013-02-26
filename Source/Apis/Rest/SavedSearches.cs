using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public static class SavedSearches
    {
        //DONE!
        //GET Methods
        
        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// </summary>
        /// <para>Avaliable parameters: Nothing </para><para> </para>
        /// <returns>Saved searches.</returns>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static IEnumerable<SavedSearch> List(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.ConvertArray<SavedSearch>(DynamicJson.Parse(
                Request.Send(Tokens, MethodType.GET, Rest.Url("saved_searches/list"), Parameters)));
        }
        
        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id. The authenticating user must be the owner of saved search ID being requested.</para>
        /// </summary>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// <returns>The saved search.</returns>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static SavedSearch Show(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.Convert<SavedSearch>(DynamicJson.Parse(
                Request.Send(Tokens, MethodType.GET, Rest.Url(string.Format("saved_searches/show/{0}", 
                    Parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString())), 
                         Parameters.Where(x => x.Parameters[0].Name != "id").ToArray())));
        }
        
        //POST Methods
        
        /// <summary>
        /// <para>Create a new saved search for the authenticated user. A user may only have 25 saved searches.</para>
        /// </summary>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para>The saved search.<paramref name="string query (required)"/> : The query of the search the user would like to save.</para>
        /// <returns></returns>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static SavedSearch Create(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.ConvertArray<SavedSearch>(DynamicJson.Parse(
                Request.Send(Tokens, MethodType.POST, Rest.Url("saved_searches/create"), Parameters)));
        }
        
        /// <summary>
        /// <para>Destroys a saved search for the authenticating user. The authenticating user must be the owner of saved search id being destroyed.</para>
        /// </summary>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// <returns>The saved search.</returns>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static SavedSearch Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.Convert<SavedSearch>(DynamicJson.Parse(
                Request.Send(Tokens, MethodType.POST, Rest.Url(string.Format("saved_searches/destroy/{0}", 
                    Parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString())),
                         Parameters.Where(x => x.Parameters[0].Name != "id").ToArray())));
        }
    }
}