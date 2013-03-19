using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    /// <summary>
    ///  GET/POST saved_searches
    /// </summary>
    public static class SavedSearches
    {
        //DONE!
        //GET Methods
        
        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// <para>Avaliable parameters: Nothing. </para>
        /// </summary>
        /// <returns>Saved searches.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static IEnumerable<SearchQuery> List(Tokens tokens, params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.ConvertArray<SearchQuery>(DynamicJson.Parse(
                Request.Send(tokens, MethodType.GET, Rest.Url("saved_searches/list"), parameters)));
        }
        
        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id. The authenticating user must be the owner of saved search ID being requested.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static SearchQuery Show(Tokens tokens, params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<SearchQuery>(DynamicJson.Parse(
                Request.Send(tokens, MethodType.GET, Rest.Url(string.Format("saved_searches/show/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString())), 
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray())));
        }
        
        //POST Methods
        
        /// <summary>
        /// <para>Create a new saved search for the authenticated user. A user may only have 25 saved searches.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string query (required)"/> : The query of the search the user would like to save.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static SearchQuery Create(Tokens tokens, params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<SearchQuery>(DynamicJson.Parse(
                Request.Send(tokens, MethodType.POST, Rest.Url("saved_searches/create"), parameters)));
        }
        
        /// <summary>
        /// <para>Destroys a saved search for the authenticating user. The authenticating user must be the owner of saved search id being destroyed.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static SearchQuery Destroy(Tokens tokens, params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<SearchQuery>(DynamicJson.Parse(
                Request.Send(tokens, MethodType.POST, Rest.Url(string.Format("saved_searches/destroy/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString())),
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray())));
        }
    }
}