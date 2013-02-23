using System;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Codeplex.Data;

namespace CoreTweet
{
    public static partial class Rest
    {
        public static class Geo
        {
            //FIXME: Doesn't know about a format of "attribute:street_address". Needed to check the format by "OAuth tool".
            
            //GET Methods
            
            /// <summary>
            /// <para>Returns all the information about a known place.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string place_id (required)"/> : A place in the world. These IDs can be retrieved from geo/reverse_geocode.</para>
            /// <returns>The geo.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Place Id(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Place>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.POST, Rest.Url(
                    string.Format("geo/id/{0}", Parameters.First(x => x.Parameters[0].Name == "place_id")
                              .Compile()("").ToString())), Parameters.Where(x => x.Parameters[0].Name != "place_id").ToArray())));
            }
            
            /// <summary>
            /// <para>Locates places near the given coordinates which are similar in name.</para>
            /// <para>Conceptually you would use this method to get a list of known places to choose from first. Then, if the desired place doesn't exist, make a request to POST geo/place to create a new one.</para>
            /// <para>The token contained in the response is the token needed to be able to create a new place.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="double lat (required)"/> : The latitude to search around. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
            /// <para><paramref name="double long (required)"/> : The longitude to search around. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
            /// <para><paramref name="string name (required)"/> : The name a place is known as.</para>
            /// <para><paramref name="string contained_within (optional)"/> : This is the place_id which you would like to restrict the search results to. Setting this value means only places within the given place_id will be found. Specify a place_id. For example, to scope all results to places within "San Francisco, CA USA", you would specify a place_id of "5a110d312052166f"</para>
            /// <para><paramref name="string attribute:street_address (optional)"/> : This parameter searches for places which have this given street address. There are other well-known, and application specific attributes available. Custom attributes are also permitted.</para>
            /// <returns>Places and the token.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static GeoResult SimilarPlaces(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<GeoResult>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("geo/similar_places"), Parameters)).result);
            }
            
            /// <summary>
            /// <para>Search for places that can be attached to a statuses/update. Given a latitude and a longitude pair, an IP address, or a name, this request will return a list of all the valid places that can be used as the place_id when updating a status.</para>
            /// <para>Conceptually, a query can be made from the user's location, retrieve a list of places, have the user validate the location he or she is at, and then send the ID of this location with a call to POST statuses/update.</para>
            /// <para>This is the recommended method to use find places that can be attached to statuses/update. Unlike GET geo/reverse_geocode which provides raw data access, this endpoint can potentially re-order places with regards to the user who is authenticated. This approach is also preferred for interactive place matching with the user.</para>
            /// </summary>
            /// <para>Note: At least one of the following parameters must be provided to this resource: lat, long, ip, or query</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="double lat (optional)"/> : The latitude to search around. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
            /// <para><paramref name="double long (optional)"/> : The longitude to search around. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
            /// <para><paramref name="string query (optional)"/> : Free-form text to match against while executing a geo-based query, best suited for finding nearby locations by name. Remember to URL encode the query.</para>
            /// <para><paramref name="string ip (optional)"/> : An IP address. Used when attempting to fix geolocation based off of the user's IP address.</para>
            /// <para><paramref name="string granularity (optional)"/> : This is the minimal granularity of place types to return and must be one of: poi, neighborhood, city, admin or country. If no granularity is provided for the request neighborhood is assumed. Setting this to city, for example, will find places which have a type of city, admin or country.</para>
            /// <para><paramref name="string accuracy (optional)"/> : A hint on the "region" in which to search. If a number, then this is a radius in meters, but it can also take a string that is suffixed with ft to specify feet. If this is not passed in, then it is assumed to be 0m. If coming from a device, in practice, this value is whatever accuracy the device has measuring its location (whether it be coming from a GPS, WiFi triangulation, etc.).</para>
            /// <para><paramref name="int max_results (optional)"/> : A hint as to the number of results to return. This does not guarantee that the number of results returned will equal max_results, but instead informs how many "nearby" results to return. Ideally, only pass in the number of places you intend to display to the user here.</para>
            /// <para><paramref name="string contained_within (optional)"/> : This is the place_id which you would like to restrict the search results to. Setting this value means only places within the given place_id will be found.</para>
            /// <para><paramref name="string attribute:street_address (optional)"/> : This parameter searches for places which have this given street address. There are other well-known, and application specific attributes available. Custom attributes are also permitted.</para>
            /// <returns>Places.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static GeoResult Search(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<GeoResult>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("geo/search"), Parameters)).result);
            }
            
            /// <summary>
            /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status.</para>
            /// <para>This request is an informative call and will deliver generalized results about geography.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="double lat (required)"/> : The latitude to search around. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
            /// <para><paramref name="double long (required)"/> : The longitude to search around. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
            /// <para><paramref name="string accuracy (optional)"/> : A hint on the "region" in which to search. If a number, then this is a radius in meters, but it can also take a string that is suffixed with ft to specify feet. If this is not passed in, then it is assumed to be 0m. If coming from a device, in practice, this value is whatever accuracy the device has measuring its location (whether it be coming from a GPS, WiFi triangulation, etc.).</para>
            /// <para><paramref name="string granularity (optional)"/> : This is the minimal granularity of place types to return and must be one of: poi, neighborhood, city, admin or country. If no granularity is provided for the request neighborhood is assumed. Setting this to city, for example, will find places which have a type of city, admin or country.</para>
            /// <para><paramref name="int max_results (optional)"/> : A hint as to the number of results to return. This does not guarantee that the number of results returned will equal max_results, but instead informs how many "nearby" results to return. Ideally, only pass in the number of places you intend to display to the user here.</para>
            /// <returns>Places.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static GeoResult ReverseGeocode(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<GeoResult>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("geo/reverse_geocode"), Parameters)).result);
            }
            
            //POST Method
            
            /// <summary>
            /// <para>Creates a new place object at the given latitude and longitude.</para>
            /// <para>Before creating a place you need to query GET geo/similar_places with the latitude, longitude and name of the place you wish to create. The query will return an array of places which are similar to the one you wish to create, and a token. If the place you wish to create isn't in the returned array you can use the token with this method to create a new one.</para>
            /// </summary>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string name (required)"/> : The name a place is known as.</para>
            /// <para><paramref name="string contained_within (required)"/> : The place_id within which the new place can be found. Try and be as close as possible with the containing place. For example, for a room in a building, set the contained_within as the building place_id.</para>
            /// <para><paramref name="string token (required)"/> : The token found in the response from geo/similar_places.</para>
            /// <para><paramref name="double lat (required)"/> : The latitude the place is located at. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
            /// <para><paramref name="double long (required)"/> : The longitude the place is located at. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
            /// <para><paramref name="attribute:street_address (optional)"/> : This parameter searches for places which have this given street address. There are other well-known, and application specific attributes available. Custom attributes are also permitted. </para>
            /// 
            /// <returns>The place.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            /// <see cref="https://dev.twitter.com/docs/finding-tweets-about-places"/>
            public static Place Place(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Place>(DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, Rest.Url("geo/reverse_geocode"), Parameters)));
            }
            
            
        }
    }
}

