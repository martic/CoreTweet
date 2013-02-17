using System;
using Codeplex.Data;
using System.Linq;
using System.Linq.Expressions;

namespace CoreTweet.Api
{
    public static class Account
    {
        
        //GET Methods
        
        /// <summary>
        ///     <para>
        ///     Returns an HTTP 200 OK response code and a representation of the requesting user if authentication was successful; returns a 401 status code and an error message if not. Use this method to test if supplied user credentials are valid.
        ///     </para>
        ///     <para>
        ///     Available parameters:
        ///     </para>
        ///     <para>
        ///     bool include_entities (optional) : The entities node will not be included when set to false.
        ///     </para>
        ///     <para>
        ///     bool skip_status (optional) : When set to either true, t or 1 statuses will not be included in the returned user objects.
        ///     </para>
        /// </summary>
        /// <returns>
        /// The user data of you.
        /// </returns>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static User VerifyCredentials(Tokens tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.Convert<User>(
                DynamicJson.Parse(
                    Request.Send(tokens, MethodType.GET, 
                         ApiList.Account.verify_credentials, Parameters)));
        }
        
        //POST Methods
        
        
        
    }
}