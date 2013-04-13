using System;
using System.Dynamic;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

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

