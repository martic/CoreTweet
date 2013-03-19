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
        /// <summary>GET trends</summary>
        public static class Trends
        {
            //DONE!
            //GET Methods
            
            /// <summary>
            /// <para>Returns the locations that Twitter has trending topic information for.</para>
            /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
            /// <para>A id is a Yahoo! Where On Earth ID.</para>
            /// <seealso cref="http://developer.yahoo.com/geo/geoplanet/"/>
            /// <para>Avaliable parameters: Nothing.</para>
            /// </summary>
            /// <returns>The locations.</returns>
            /// <param name='tokens'>
            /// Tokens.
            /// </param>
            /// <param name='parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Place> Avaliable(Tokens tokens, params Expression<Func<string,object>>[] parameters)
            {
                return CoreBase.ConvertArray<Place>(DynamicJson.Parse(
                    Request.Send(tokens, MethodType.GET, Rest.Url("trends/avaliable"), parameters)));
            }
            
            /// <summary>
            /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location.</para>
            /// <para>The response is an array of "locations" that encode the location's Id and some other human-readable information such as a canonical name and country the location belongs in.</para>
            /// <para>A id is a Yahoo! Where On Earth ID.</para>
            /// <seealso cref="http://developer.yahoo.com/geo/geoplanet/"/>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="double lat (required)"/> : If provided with a long parameter the available trend locations will be sorted by distance, nearest to furthest, to the co-ordinate pair. The valid ranges for longitude is -180.0 to +180.0 (West is negative, East is positive) inclusive.</para>
            /// <para><paramref name="double long (required)"/> : If provided with a lat parameter the available trend locations will be sorted by distance, nearest to furthest, to the co-ordinate pair. The valid ranges for longitude is -180.0 to +180.0 (West is negative, East is positive) inclusive.</para>
            /// </summary>
            /// <returns>The locations.</returns>
            /// <param name='tokens'>
            /// Tokens.
            /// </param>
            /// <param name='parameters'>
            /// Parameters.
            /// </param>
            public static IEnumerable<Place> Closest(Tokens tokens, params Expression<Func<string,object>>[] parameters)
            {
                return CoreBase.ConvertArray<Place>(DynamicJson.Parse(
                    Request.Send(tokens, MethodType.GET, Rest.Url("trends/closest"), parameters)));
            }
            
            /// <summary>
            /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it.</para>
            /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
            /// <para>This information is cached for 5 minutes. Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="long id (required)"/> : The Yahoo! Where On Earth ID of the location to return trending information for. Global information is available by using 1 as the WOEID.</para>
            /// </summary>
            /// <returns>The queries.</returns>
            /// <param name='tokens'>
            /// Tokens.
            /// </param>
            /// <param name='parameters'>
            /// Parameters.
            /// </param>
            public static TrendsResult Place(Tokens tokens, params Expression<Func<string,object>>[] parameters)
            {
                return CoreBase.Convert<TrendsResult>(DynamicJson.Parse(
                    Request.Send(tokens, MethodType.GET, Rest.Url("trends/place"), parameters)));
            }
        }
    }
}
