using System;
using Codeplex.Data;
using System.Linq;
using System.Linq.Expressions;

namespace CoreTweet
{
    public static partial class Api
    {
        public static class Statuses
        {
            
            //GET Methods

            /// <summary>
            /// <para>Returns the 20 most recent mentions (tweets containing a users's @screen_name) for the authenticating user. The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com. This method can only return up to 800 tweets. See Working with Timelines for instructions on traversing timelines.</para>
            /// <seealso cref="https://dev.twitter.com/docs/working-with-timelines"/>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
            /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
            /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
            /// </summary>
            /// <param name="Tokens">OAuth Tokens.</param>
            /// <param name="Parameters">Parameters.</param>
            /// <returns>The statuses.</returns>
            public static Status[] MentionsTimeline(Tokens Tokens, params Expression<Func<string, object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.GET, TwiTool.GetAPIURL("statuses/mentions_timeline"), Parameters)));
            }

            /// <summary>
            /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters. User timelines belonging to protected users may only be requested when the authenticated user either "owns" the timeline or is an approved follower of the owner. The timeline returned is the equivalent of the one seen when you view a user's profile on twitter.com. This method can only return up to 3,200 of a user's most recent Tweets. Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="int user_id (optional)"/> : The ID of the user for whom to return results for.</para>
            /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
            /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="bool trim_user (optional)"/> : When set to true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
            /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
            /// <para><paramref name="bool include_rts (optional)"/> : When set to false, the timeline will strip any native retweets (though they will still count toward both the maximal length of the timeline and the slice selected by the count parameter). Note: If you're using the trim_user parameter in conjunction with include_rts, the retweets will still contain a full user object.</para>
            /// <para><paramref name="bool exclude_replies (optional)"/> : This parameter will prevent replies from appearing in the returned timeline. Using exclude_replies with the count parameter will mean you will receive up-to count tweets — this is because the count parameter retrieves that many tweets before filtering out retweets and replies. This parameter is only supported for JSON and XML responses.</para>
            /// </summary>
            /// <param name="Tokens">OAuth Tokens.</param>
            /// <param name="Parameters">Parameters.</param>
            /// <returns>The statuses.</returns>
            public static Status[] UserTimeline(Tokens Tokens, params Expression<Func<string, object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.GET, TwiTool.GetAPIURL("statuses/user_timeline"), Parameters)));
            }

            /// <summary>
            /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow. The home timeline is central to how most users interact with the Twitter service. Up to 800 Tweets are obtainable on the home timeline. It is more volatile for users that follow many users or follow users who tweet frequently.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
            /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="bool trim_user (optional)"/> : When set to true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
            /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
            /// <para><paramref name="bool exclude_replies (optional)"/> : This parameter will prevent replies from appearing in the returned timeline. Using exclude_replies with the count parameter will mean you will receive up-to count tweets — this is because the count parameter retrieves that many tweets before filtering out retweets and replies. This parameter is only supported for JSON and XML responses.</para>
            /// </summary>
            /// </summary>
            /// <param name="Tokens">OAuth Tokens.</param>
            /// <param name="Parameters">Parameters.</param>
            /// <returns>The statuses.</returns>
            public static Status[] HomeTimeline(Tokens Tokens, params Expression<Func<string, object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.GET, TwiTool.GetAPIURL("statuses/home_timeline"), Parameters)));
            }

            /// <summary>
            /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others. This timeline is a subset of the user's GET statuses/user_timeline.</para>
            /// <seealso cref="https://dev.twitter.com/docs/working-with-timelines"/>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
            /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The tweet entities node will be disincluded when set to false.</para>
            /// <para><paramref name="bool include_user_entities (optional)"/> :The user entities node will be disincluded when set to false..</para>
            /// </summary>
            /// <param name="Tokens">OAuth Tokens.</param>
            /// <param name="Parameters">Parameters.</param>
            /// <returns>The statuses.</returns>
            public static Status[] RetweetsOfMe(Tokens Tokens, params Expression<Func<string, object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.GET, TwiTool.GetAPIURL("statuses/retweets_of_me"), Parameters)));
            }

            //POST Methods

            /// <summary>
            /// <para>Updates the authenticating user's current status, also known as tweeting. To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
            /// <para>For each update attempt, the update text is compared with the authenticating user's recent tweets. Any attempt that would result in duplication will be blocked, resulting in a 403 error. Therefore, a user cannot submit the same status twice in a row.</para>
            /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time. If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
            /// <para></para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string status (required)"/> : The text of your status update, typically up to 140 characters. URL encode as necessary. t.co link wrapping may effect character counts.</para>
            /// <para><paramref name="long in_reply_to_status_id (optional)"/> : The ID of an existing status that the update is in reply to.</para>
            /// <para><paramref name="double lat (optional)"/> : The latitude of the location this tweet refers to. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
            /// <para><paramref name="double long (optional)"/> : The longitude of the location this tweet refers to. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
            /// <para><paramref name="string place_id (optional)"/> : A place in the world. These IDs can be retrieved from GET geo/reverse_geocode.</para>
            /// <para><paramref name="bool display_coordinates (optional)"/> : Whether or not to put a pin on the exact coordinates a tweet has been sent from.</para>
            /// <para><paramref name="bool trim_user (optional)"/> : When set to true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
            /// </summary>
            /// <returns>The updated status.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Status Update(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                
                return CoreBase.Convert<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("statuses/update"), Parameters)));
            }
        }
    }
}

