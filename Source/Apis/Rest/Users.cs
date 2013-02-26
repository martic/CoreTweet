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
        public static class Users
        {
            //DONE!
            //GET Methods
            
            /// <summary>
            /// <para>Returns a collection of users that the specified user can "contribute" to.</para>
            /// </summary>
            /// <para>Note: A user_id or screen_name is required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<User> Contributees(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/contributees"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns a collection of users who can contribute to the specified account.</para>
            /// </summary>
            /// <para>Note: A user_id or screen_name is required.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<User> Contributors(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/contributors"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
            /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
            /// <para>GET users/show is used to retrieve a single user object.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string user_id (optional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger requests.</para>
            /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger (up to 100 screen names) requests.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node that may appear within embedded statuses will be disincluded when set to false.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<User> Lookup(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/lookup"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns the size of the specified user's profile banner. If the user has not uploaded a profile banner, a HTTP 404 will be served instead. This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
            /// </summary>
            /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <returns>The size.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Size ProfileBanner(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Size>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/profile_banner"), Parameters)).web);
            }
            
            /// <summary>
            /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter. Try querying by topical interest, full name, company name, location, or other criteria. Exact match searches are not supported.</para>
            /// <para>Only the first 1,000 matching results are available.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string q (required)"/> : The search query to run against people search.</para>
            /// <para><paramref name="iint page (optional)"/> : Specifies the page of results to retrieve.</para>
            /// <para><paramref name="int count (optional)"/> : The number of potential user results to retrieve per page. This value has a maximum of 20.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded from embedded tweet objects when set to false.</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<User> Search(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/search"), Parameters)));
            }
            
            /// <summary>
            /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter. The author's most recent Tweet will be returned inline when possible.</para>
            /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Either an id or screen_name is required for this method.</para>
            /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to return results for. Either a id or screen_name is required for this method.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
            /// <returns></returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User Show(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/show"), Parameters)));
            }
            
            /// <summary>
            /// <para>Access to Twitter's suggested user list. This returns the list of suggested user categories. The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string lang (optional)"/> : Restricts the suggested categories to the requested language. The language must be specified by the appropriate two letter ISO 639-1 representation. Currently supported languages are provided by the GET help/languages API request. Unsupported language codes will receive English (en) results. If you use lang in this request, ensure you also include it when requesting the GET users/suggestions/:slug list.</para>
            /// <returns>Catgories.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Category> Suggestions(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Category>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("users/suggestions"), Parameters)));
            }
            
            /// <summary>
            /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string slug (required)"/> : The short name of list or a category</para>
            /// <returns>Users.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<User> SuggestedMembers(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                Request.Send(Tokens, MethodType.GET, Rest.Url(string.Format("users/suggestions/{0}/members", 
                    Parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString())), 
                         Parameters.Where(x => x.Parameters[0].Name != "id").ToArray())));
            }
            
            //POST Method
            
            /// <summary>
            /// <para>Report the specified user as a spam account to Twitter. Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user.</para>
            /// </summary>
            /// <para>Note: One of these parameters must be provided.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string screen_name (optional)"/> : The ID or screen_name of the user you want to report as a spammer. Helpful for disambiguating when a valid screen name is also a user ID.</para>
            /// <para><paramref name="long user_id (optional)"/> : The ID of the user you want to report as a spammer. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
            /// <returns>The User.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User ReportSpam(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url("users/report_spam"), Parameters)));
            }
        }
    }
}