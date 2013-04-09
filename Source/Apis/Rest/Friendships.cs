using System;
using CoreTweet.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codeplex.Data;

namespace CoreTweet.Core
{

    /// <summary>GET/POST Friendships</summary>
    public class Friendships : TokenIncluded
    {
        internal Friendships(Tokens e) : base(e) { }
        //DONE!
            
        //GET Methods

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Avaliable parameters: Nothing.</para>
        /// </summary>
        /// <returns>
        /// Ids.
        /// </returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<long> NoRetweetsIds()
        {
            return ((long[])DynamicJson.Parse(Request.Send(this.Tokens, MethodType.GET, Tokens.Url("friendships/no_retweets/ids"), new Expression<Func<string,object>>[0])));
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <returns>IDs.</returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<long> Incoming(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<Cursored<long>>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.GET, Tokens.Url("friendships/incoming"), parameters)));
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of connections to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page."The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <returns>IDs.</returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<long> Outgoing(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<Cursored<long>>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.GET, Tokens.Url("friendships/outgoing"), parameters)));
        }
   
        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided. Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string user_id (ooptional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request.</para>
        /// </summary>
        /// <returns>The Friendships.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Friendship> Lookup(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.ConvertArray<Friendship>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.GET, Tokens.Url("friendships/lookup"), parameters)));
        }
            
        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long source_id (optional)"/> : The user_id of the subject user.</para>
        /// <para><paramref name="string source_screen_name (optional)"/> : The screen_name of the subject user.</para>
        /// <para><paramref name="long target_id (optional)"/> : The user_id of the target user.</para>
        /// <para><paramref name="string target_screen_name (optional)"/> : The screen_name of the target user.</para>
        /// </summary>
        /// <returns>The relationship.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public RelationShip Show(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<RelationShip>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.GET, Tokens.Url("friendships/show"), parameters)));
        }
            
        //POST Methods
            
        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter.</para>
        /// <para>Returns the befriended user in the requested format when successful. Returns a string describing the failure condition when unsuccessful. If you are already friends with the user a HTTP 403 may be returned, though for performance reasons you may get a 200 OK message even if the friendship already exists.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to befriend.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to befriend.</para>
        /// <para><paramref name="bool follow (optional)"/> : Enable notifications for the target user.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Create(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<User>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.POST, Tokens.Url("friendships/create"), parameters)));
        }
            
        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter.</para>
        /// <para>Returns the unfollowed user in the requested format when successful. Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to unfollow.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to unfollow.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<User>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.POST, Tokens.Url("friendships/destroy"), parameters)));
        }
            
            
        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to befriend.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to befriend.</para>/// <para><paramref name=""/> :</para>
        /// <para><paramref name="bool device (optional)"/> : Enable/disable device notifications from the target user.</para>
        /// <para><paramref name="bool retweets (optional)"/> : Enable/disable retweets from the target user.</para>
        /// </summary>
        /// <returns>The relationship.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public RelationShip Update(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<RelationShip>(this.Tokens, DynamicJson.Parse(Request.Send(this.Tokens, MethodType.POST, Tokens.Url("friendships/update"), parameters)));
        }

    }
}
