using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet.Rest
{

    ///<summary>GET search</summary>
    public class Search : TokenIncluded
    {
        internal Search(Tokens e) : base(e) { }
            
        //DONE!
        //GET Method
            
        /// <summary>
        /// <para>Returns a collection of relevant Tweets matching a specified query.</para>
        /// <para>Please note that Twitter's search service and, by extension, the Search API is not meant to be an exhaustive source of Tweets. Not all Tweets will be indexed or made available via the search interface.</para>
        /// <see cref="https://dev.twitter.com/docs/using-search"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string q (required)"/> : A UTF-8, URL-encoded search query of 1,000 characters maximum, including operators. Queries may additionally be limited by complexity.</para>
        /// <para><paramref name="string geocode (optional)"/> : Returns tweets by users located within a given radius of the given latitude/longitude. The location is preferentially taking from the Geotagging API, but will fall back to their Twitter profile. The parameter value is specified by "latitude,longitude,radius", where radius units must be specified as either "mi" (miles) or "km" (kilometers). Note that you cannot use the near operator via the API to geocode arbitrary locations; however you can use this geocode parameter to search near geocodes directly. A maximum of 1,000 distinct "sub-regions" will be considered when using the radius modifier.</para>
        /// <para><paramref name="string lang (optional)"/> : Restricts tweets to the given language, given by an ISO 639-1 code. Language detection is best-effort.</para>
        /// <para><paramref name="string locale (optional)"/> : Specify the language of the query you are sending (only ja is currently effective). This is intended for language-specific consumers and the default should work in the majority of cases.</para>
        /// <para><paramref name="string result_type (optional)"/> : Optional. Specifies what type of search results you would prefer to receive. The current default is "mixed." Valid values include: * mixed: Include both popular and real time results in the response. * recent: return only the most recent results in the response. * popular: return only the most popular results in the response.</para>
        /// <para><paramref name="int count (optional)"/> : The number of tweets to return per page, up to a maximum of 100. Defaults to 15.</para>
        /// <para><paramref name="string until (optional)"/> : Returns tweets generated before the given date. Date should be formatted as YYYY-MM-DD. Keep in mind that the search index may not go back as far as the date you specify here.</para>
        /// <para><paramref name="long since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="long max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// </summary>
        /// <returns>Statuses.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        internal IEnumerable<Status> Tweets(params Expression<Func<string,object>>[] parameters)
        {
            return CoreBase.ConvertArray<Status>(this.Tokens, DynamicJson.Parse(
                this.Tokens.SendRequest(MethodType.Get, "search/tweets", parameters)).statuses);
        }
    }
}
