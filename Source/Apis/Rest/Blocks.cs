using System;
using System.Linq.Expressions;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet.Core
{

    ///<summary>GET/POST blocks</summary>
    public class Blocks : _Tokens
    {
        internal Blocks() { }
        //DONE!
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <returns>IDs.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<long> Ids(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<Cursored<long>>(DynamicJson.Parse(
                    Request.Send(this, MethodType.GET, Tokens.Url("blocks/ids"), parameters)));
        }
            
        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of blocked users to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<User> List(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<Cursored<User>>(DynamicJson.Parse(
                    Request.Send(this, MethodType.GET, Tokens.Url("blocks/list"), parameters)));
        }
            
        //POST Methods
            
        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user. In addition the blocked user will not show in the authenticating users mentions or timeline (unless retweeted by another user). If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Create(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(this, MethodType.POST, Tokens.Url("blocks/create"), parameters)));
        }
            
        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user. Returns the un-blocked user in the requested format when successful. If relationships existed before the block was instated, they will not be restored.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(this, MethodType.POST, Tokens.Url("blocks/destroy"), parameters)));
        }
    }
}