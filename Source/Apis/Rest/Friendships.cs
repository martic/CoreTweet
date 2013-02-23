using System;
using CoreTweet.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codeplex.Data;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Friendships
        {
            //DONE!
            
            //GET Methods

            /// <summary>
            /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from.</para>
            /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
            /// </summary>
            /// <para>Avaliable parameters: Nothing.</para>
            /// <returns>
            /// Ids.
            /// </returns>
            /// <param name='Tokens'>
            /// OAuth Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<long> NoRetweetsIds(Tokens Tokens)
            {
                return ((long[])DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friendships/no_retweets/ids"), new Expression<Func<string,object>>[0])));
            }

            /// <summary>
            /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <returns>IDs.</returns>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<long> Incoming(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<long>>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friendships/incoming"), Parameters)));
            }

            /// <summary>
            /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page."The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <param name='Tokens'>
            /// Oauth Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <returns>IDs.</returns>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<long> Outgoing(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<long>>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friendships/outgoing"), Parameters)));
            }
   
            /// <summary>
            /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided. Values for connections can be: following, following_requested, followed_by, none.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request.</para>
            /// <para><paramref name="string user_id (ooptional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request.</para>
            /// <returns>The Friendships.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Friendship> Lookup(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Friendship>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friendships/lookup"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns detailed information about the relationship between two arbitrary users.</para>
            /// </summary>
            /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long source_id (optional)"/> : The user_id of the subject user.</para>
            /// <para><paramref name="string source_screen_name (optional)"/> : The screen_name of the subject user.</para>
            /// <para><paramref name="long target_id (optional)"/> : The user_id of the target user.</para>
            /// <para><paramref name="string target_screen_name (optional)"/> : The screen_name of the target user.</para>
            /// <returns>The relationship.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static RelationShip Show(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<RelationShip>(DynamicJson.Parse(Request.Send(Tokens, MethodType.GET, Rest.Url("friendships/show"), Parameters)));
            }
            
            //POST Methods
            
            /// <summary>
            /// <para>Allows the authenticating users to follow the user specified in the ID parameter.</para>
            /// <para>Returns the befriended user in the requested format when successful. Returns a string describing the failure condition when unsuccessful. If you are already friends with the user a HTTP 403 may be returned, though for performance reasons you may get a 200 OK message even if the friendship already exists.</para>
            /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
            /// </summary>
            /// <para>Note: Providing either screen_name or user_id is required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to befriend.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to befriend.</para>
            /// <para><paramref name="bool follow (optional)"/> : Enable notifications for the target user.</para>
            /// <returns>The user.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User Create(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(Request.Send(Tokens, MethodType.POST, Rest.Url("friendships/create"), Parameters)));
            }
            
            /// <summary>
            /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter.</para>
            /// <para>Returns the unfollowed user in the requested format when successful. Returns a string describing the failure condition when unsuccessful.</para>
            /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
            /// </summary>
            /// <para>Note: Providing either screen_name or user_id is required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to unfollow.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to unfollow.</para>
            /// <returns>The user.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(Request.Send(Tokens, MethodType.POST, Rest.Url("friendships/destroy"), Parameters)));
            }
            
            
            /// <summary>
            /// <para>Allows one to enable or disable retweets and device notifications from the specified user.</para>
            /// </summary>
            /// <para>Note: Providing either screen_name or user_id is required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to befriend.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to befriend.</para>/// <para><paramref name=""/> :</para>
            /// <para><paramref name="bool device (optional)"/> : Enable/disable device notifications from the target user.</para>
            /// <para><paramref name="bool retweets (optional)"/> : Enable/disable retweets from the target user.</para>
            /// <returns>The relationship.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static RelationShip Update(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<RelationShip>(DynamicJson.Parse(Request.Send(Tokens, MethodType.POST, Rest.Url("friendships/update"), Parameters)));
            }

        }
    }
}
