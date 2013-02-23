using System;
using CoreTweet.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codeplex.Data;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class DirectMessages
        {
            //DONE!
            
            //GET Methods
            
            /// <summary>
            /// <para>Returns the 20 most recent direct messages sent by the authenticating user. Includes detailed information about the sender and recipient user. You can request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs.</para>
            /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="long max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of records to retrieve. Must be less than or equal to 200.<\para>
            /// <para><paramref name="int page (optional)"/> : Specifies the page of results to retrieve.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <returns>Direct messages.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<DirectMessage> Sent(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<DirectMessage>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("direct_messages/sent"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns a single direct message, specified by an id parameter. Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
            /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (required)"/> : The ID of the direct message.</para>
            /// <returns>Direct messages.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<DirectMessage> Show(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<DirectMessage>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("direct_messages/show"), Parameters)));
            }
            
            //POST Methods
            
            /// <summary>
            /// <para>Sends a new direct message to the specified user from the authenticating user. Requires both the user and text parameters and must be a POST. Returns the sent message in the requested format if successful.</para>
            /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
            /// </summary>
            /// <para>Note: One of user_id or screen_name are required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user who should receive the direct message. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user who should receive the direct message. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="string text (required)"/> : The text of your direct message. Be sure to URL encode as necessary, and keep the message under 140 characters.</para>
            /// <returns>The sent direct message.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static DirectMessage New(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<DirectMessage>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url("direct_messages/new"), Parameters)));
            }
            
            /// <summary>
            /// <para>Destroys the direct message specified in the required ID parameter. The authenticating user must be the recipient of the specified direct message.</para>
            /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (required)"/> : The ID of the direct message to delete.</para>
            /// <para><paramref name="bool include_entities"/> : The entities node will not be included when set to false.</para>
            /// <returns>THe direct message.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static DirectMessage Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<DirectMessage>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url("direct_messages/destroy"), Parameters)));
            }
        }
    }
}
