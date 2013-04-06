using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet;
using CoreTweet.Core;

/// <summary>
/// The powerful extensions for CoreTweet.
/// </summary>
namespace CoreTweet.Ex
{
    public static class SearchExtension
    {
        public static IEnumerable<Status> SearchTweets(this SearchQuery e, params Expression<Func<string,object>>[] parameters)
        {
            return e._.Search.Tweets((parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{q => e.Query})
                                       .ToArray());
        }
        
        public static IEnumerable<User> SearchUsers(this SearchQuery e, Tokens Tokens, params Expression<Func<string,object>>[] parameters)
        {
            return e._.Users.Search((parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{q => e.Query})
                                       .ToArray());
        }
    }
}

