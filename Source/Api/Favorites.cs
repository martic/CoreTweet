using System;
using System.Linq;
using System.Linq.Expressions;
using Codeplex.Data;

namespace CoreTweet
{
    public static partial class Api
    {
        public static class Favorites
        {
            //DONE!
            
            //GET Method
            
            /// <summary>
            /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (optional)"/> : The ID of the user for whom to return results for.</para>
            /// <para><paramref name="string screen_name (optonal)"/> : The screen name of the user for whom to return results for.</para>
            /// <para><paramref name="int count (optional)"/> : Specifies the number of records to retrieve. Must be less than or equal to 200. Defaults to 20.</para>
            /// <para><paramref name="int since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
            /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
            /// <returns>The statuses.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Status[] List(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.ConvertArray<Status>(DynamicJson.Parse(
                         Request.Send(Tokens, MethodType.GET, TwiTool.GetAPIURL("favorites/list"), Parameters)));
            }  
            
            //POST Methods
            
            /// <summary>
            /// <para>Favorites the status specified in the ID parameter as the authenticating user. Returns the favorite status when successful.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
            /// <para><paramref name="bool include_entities (ooptional)"/> : The entities node will be omitted when set to false.</para>
            /// <returns>The favorited status.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Status Create(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Status>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("favorites/create"), Parameters)));
            }
            
            /// <summary>
            /// <para>Un-favorites the status specified in the ID parameter as the authenticating user. Returns the un-favorited status in the requested format when successful.</para>
            /// <para>This process invoked by this method is asynchronous. The immediately returned status may not indicate the resultant favorited status of the tweet. A 200 OK response from this method will indicate whether the intended action was successful or not.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
            /// <para><paramref name="bool include_entities (ooptional)"/> : The entities node will be omitted when set to false.</para>
            /// <returns>The destroied status.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Status Destroy(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Status>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("favorites/destroy"), Parameters)));
            }
        }
    }
}