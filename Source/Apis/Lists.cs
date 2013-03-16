using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Lists
        {
            //GET Methods
            
            /// <summary>
            /// <para>Returns all lists the authenticating or specified user subscribes to, including their own. The user is specified using the user_id or screen_name parameters. If no user is given, the authenticating user is used.</para>
            /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <returns>Lists.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<CoreTweet.List> List(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<CoreTweet.List>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("lists/list"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns the members of the specified list. Private list members will only be shown if the authenticated user owns the specified list.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
            /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
            /// <para><paramref name="string owner_sereen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Causes the collection of list members to be broken into "pages" of somewhat consistent size. If no cursor is provided, a value of -1 will be assumed, which is the first "page".</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Cursored<User> Members(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<User>>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.GET, Rest.Url("lists/members"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns the lists the specified user has been added to. If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
            /// </summary>
            /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string sereen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="long cursor (semi-optional)"/> : Breaks the results into pages. A single page contains 20 lists. Provide a value of -1 to begin paging. Provide values as returned in the response body's next_cursor and previous_cursor attributes to page back and forth in the list.</para>
            /// <para><paramref name="bool filter_to_owned_lists (optional)"/> : When set to true, will return just lists the authenticating user owns, and the user represented by user_id or screen_name is a member of.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Cursored<User> Memberships(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<User>>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.GET, Rest.Url("lists/memberships"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns the specified list. Private lists will only be shown if the authenticated user owns the specified list.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
            /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
            /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static CoreTweet.List Show(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<CoreTweet.List>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.GET, Rest.Url("lists/show"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns tweet timeline for members of the specified list. Retweets are included by default. You can use the include_rts=false parameter to omit retweet objects.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
            /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
            /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="long max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of results to retrieve per page.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : Entities are ON by default in API 1.1, each tweet includes a node called entities. This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. You can omit entities from the result by using include_entities=false.<\para>
            /// <para><paramref name="bool include_rts (optional)"/> : When set to true, the list timeline will contain native retweets (if they exist) in addition to the standard stream of tweets. The output format of retweeted tweets is identical to the representation you see in home_timeline.</para>
            /// <returns>Statuses.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Status> Statuses(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.GET, Rest.Url("lists/statuses"), Parameters)));
            }
            
            /// <summary>
            /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default. Does not include the user's own lists.</para>
            /// </summary>
            /// <para>Note: A user_id or screen_name must be provided.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="int count (optional)"/> : The amount of results to return per page. Defaults to 20. Maximum of 1,000 when using cursors.</para>
            /// <para><paramref name="long cursor (optional)"/> : Breaks the results into pages. A single page contains 20 lists. Provide a value of -1 to begin paging. Provide values as returned in the response body's next_cursor and previous_cursor attributes to page back and forth in the list. It is recommended to always use cursors when the method supports them.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Cursored<CoreTweet.List> Subscriptions(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Cursored<CoreTweet.List>>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("lists/subscriptions"), Parameters)));
            }
            
            /// <summary>
            /// <para>Check if the specified user is a member of the specified list.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
            /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
            /// <para><paramref name="string sereen_name (required)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User MembersShow(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("lists/members/show"), Parameters)));
            }
            
            /// <summary>
            /// <para>Check if the specified user is a subscriber of the specified list. Returns the user if they are subscriber.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
            /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
            /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
            /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="bool include_entities"/> : When set to true, each tweet will include a node called "entities". This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. While entities are opt-in on timelines at present, they will be made a default component of output in the future. See Tweet Entities for more details.</para>
            /// <para><paramref name="bool skip_status"/> : When set to true, statuses will not be included in the returned user objects.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </pasram>
            public static User SubscribersShow(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("lists/subscribers/show"), Parameters)));
            }
            
            //POST Methods
            
            /// <summary>
            /// <para>Creates a new list for the authenticated user. Note that you can't create more than 20 lists per account.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string name (required)"/> : The name for the list.A list's name must start with a letter and can consist only of 25 or fewer letters, numbers, "-", or "_" characters.</para>
            /// <para><paramref name="string mode (optional)"/> : Whether your list is public or private. Values can be public or private. If no mode is specified the list will be public.</para>
            /// <para><paramref name="string description (optional)"/> : The description to give the list.</para>
            /// <returns>The list.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static CoreTweet.List Create(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<CoreTweet.List>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.POST, Rest.Url("lists/create"), Parameters)));
            }
            
            /// <summary>
            /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
            /// </summary>
            /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static CoreTweet.List Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<CoreTweet.List>(DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.POST, Rest.Url("lists/destroy"), Parameters)));
            }
        }
    }
}