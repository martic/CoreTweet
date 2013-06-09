using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet.Rest
{

    
    /// <summary>
    ///  GET/POST saved_searches
    /// </summary>
    public class SavedSearches : TokenIncluded
    {
        internal SavedSearches(Tokens e) : base(e) { }
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
        public IEnumerable<SearchQuery> List(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<SearchQuery>(MethodType.Get, "saved_searches/list", parameters);
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
        public SearchQuery Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Get, string.Format("saved_searches/show/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()), 
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
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
        public SearchQuery Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Post, "saved_searches/create", parameters);
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
        public SearchQuery Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Post, string.Format("saved_searches/destroy/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()),
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
    }    
}
