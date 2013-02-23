using System;
using System.Linq.Expressions;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Blocks
        {
            //DONE!
            
            //GET Methods
            
            /// <summary>
            /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<long> Ids(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<long>>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("blocks/ids"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of blocked users to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            public static Cursored<User> List(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<User>>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("blocks/list"), Parameters)));
            }
            
            //POST Methods
            
            /// <summary>
            /// <para>Blocks the specified user from following the authenticating user. In addition the blocked user will not show in the authenticating users mentions or timeline (unless retweeted by another user). If a follow or friend relationship exists it is destroyed.</para>
            /// </summary>
            /// <para>Note: Either screen_name or user_id must be provided.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
            /// <returns>The user.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User Create(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url("blocks/create"), Parameters)));
            }
            
            /// <summary>
            /// <para>Un-blocks the user specified in the ID parameter for the authenticating user. Returns the un-blocked user in the requested format when successful. If relationships existed before the block was instated, they will not be restored.</para>
            /// </summary>
            /// <para>Note: Either screen_name or user_id must be provided.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
            /// <returns>The user.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url("blocks/destroy"), Parameters)));
            }
        }
    }
}