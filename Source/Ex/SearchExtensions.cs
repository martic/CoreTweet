using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Ex
{
    public static class SearchExtension
    {
        public static IEnumerable<Status> SearchTweets(this SearchQuery e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Search.Tweets(Tokens,
                               (Parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{q => e.Query})
                                       .ToArray());
        }
        
        public static IEnumerable<User> SearchUsers(this SearchQuery e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Users.Search(Tokens,
                               (Parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{q => e.Query})
                                       .ToArray());
        }
    }
}

