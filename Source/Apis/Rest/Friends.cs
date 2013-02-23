using System;
using System.Linq.Expressions;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Friends
        {
            //DONE!

            //GET Methods

            /// <summary>
            /// <para>Returns a cursored collection of user IDs for every user the specified user is following (otherwise known as their "friends").</para>
            /// <para>At this time, results are ordered with the most recent following first - however, this ordering is subject to unannounced change and eventual consistency issues. Results are given in groups of 5,000 user IDs and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests. See Using cursors to navigate collections for more information.</para>
            /// <para>This method is especially powerful when used in conjunction with GET users/lookup, a method that allows you to convert user IDs into full user objects in bulk.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page". The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of IDs attempt retrieval of, up to a maximum of 5,000 per distinct request. The value of count is best thought of as a limit to the number of results to return. When using the count parameter with this method, it is wise to use a consistent count value across all requests to the same user's collection. Usage of this parameter is encouraged in environments where all 5,000 IDs constitutes too large of a response.</para>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name="Parameters">Parameters.</param>
            /// <returns>IDs.</returns>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<long> Ids(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<long>>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friends/ids"), Parameters)));
            }

            /// <summary>
            /// <para>Returns a cursored collection of user objects for every user the specified user is following (otherwise known as their "friends").</para>
            /// <para>At this time, results are ordered with the most recent following first — however, this ordering is subject to unannounced change and eventual consistency issues. Results are given in groups of 20 users and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests. See Using cursors to navigate collections for more information.</para>
            /// </summary>
            /// <para>Note: Either a screen_name or a user_id should be provided.</para>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="long id (optional)"/> : The ID of the user for whom to return results for.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page". The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
            /// <para><paramref name="bool include_user_entities"/> : The user object entities node will be disincluded when set to false.</para>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <returns>Users.</returns>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<User> List(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<User>>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friends/list"), Parameters)));
            }
        }
    }
}