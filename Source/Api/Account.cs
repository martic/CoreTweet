using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoreTweet.Api
{
    public static class Account
    {
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
        /// OAuth Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static User VerifyCredentials(Tokens tokens, params Expression<Func<string,bool>>[] Parameters)
        {
            Request.Send(tokens, MethodType.GET, "https://api.twitter.com/1.1/account/verify_credentials.json", Parameters);
        }
        
    }
}