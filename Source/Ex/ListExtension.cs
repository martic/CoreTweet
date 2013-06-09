using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Alice.Extensions;

namespace CoreTweet.Ex
{
    public static class ListExtension
    {
        public static List AddUser(this List e, params User[] target)
        {
            e = e.Tokens.Lists.Members.CreateAll(list_id => e.Id, user_id => target.Select(x => x.Id).JoinToString(","));
            return e;
        }
        
        public static Cursored<User> GetMembers(this List e, params Expression<Func<string,object>>[] parameters)
        {
            return e.Tokens.Lists.Members.This((parameters as IEnumerable<Expression<Func<string,object>>>)
                                              .Union(new Expression<Func<string,object>>[]{list_id => e.Id})
                                              .ToArray());
        }
        
        public static List DeleteMember(this List e, params User[] target)
        {
            e = e.Tokens.Lists.Members.DeleteAll(list_id => e.Id, user_id => target.Select(x => x.Id).JoinToString(","));
            return e;
        }
    }
}

