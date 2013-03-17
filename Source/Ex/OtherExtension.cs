using System;
using System.Dynamic;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

/// <summary>
/// The powerful extensions for CoreTweet.
/// </summary>
namespace CoreTweet.Ex
{
    /// <summary>
    /// Extensions for other object.
    /// </summary>
    public static class OthersExtension
    {
        /// <summary>
        /// Tweet this text.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long in_reply_to_status_id (optional)"/> : The ID of an existing status that the update is in reply to.</para>
        /// <para><paramref name="double lat (optional)"/> : The latitude of the location this tweet refers to. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
        /// <para><paramref name="double long (optional)"/> : The longitude of the location this tweet refers to. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
        /// <para><paramref name="string place_id (optional)"/> : A place in the world. These IDs can be retrieved from GET geo/reverse_geocode.</para>
        /// <para><paramref name="bool display_coordinates (optional)"/> : Whether or not to put a pin on the exact coordinates a tweet has been sent from.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static Status Tweet(this string e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Statuses.Update(Tokens,
                               (Parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[] {status => e})
                                       .ToArray());
        }
    }
}

/// <summary>
/// Extentions for developer.
/// There is no need for end users to use it.
/// </summary>
namespace CoreTweet.Ex.Develop
{
    /// <summary>
    /// Extentions for developer.
    /// </summary>
    public static class DevelopersExtention
    {
        /// <summary>
        /// Replaces the bad charactor on the specified JSON.
        /// </summary>
        /// <returns>The 'good' JSON.</returns>
        /// <param name="json">Json.</param>
        public static string ReplaceBadCharactor(this string json)
        {
            foreach(System.Text.RegularExpressions.Match x in 
                    new System.Text.RegularExpressions.Regex(@"\d+\:id").Matches(json))
                json = json.Replace(x.Value, "id");
            return json;
        }

        /// <summary>
        /// Converts the specified dynamic object to dictionary.
        /// </summary>
        /// <returns>The dictionary.</returns>
        /// <param name="e">The dynamic object.</param>
        public static IDictionary<string,object> ToDictionary(DynamicJson e)
        {
            return (e.GetDynamicMemberNames() as IEnumerable<string>)
                .ToDictionary(x => x, y => {
                object f;
                e.TryGetMember(y, out f);
                return f;});
        }
    }
}

