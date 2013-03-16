using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Alice;

namespace CoreTweet.Ex
{
    public static class UserExtension
    {
        /// <summary>
        /// Follow this user.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="bool follow (optional)"/> : Enable notifications for the target user.</para>
        public static User Follow(this User e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Friendships.Create(Tokens,
                               (Parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{user_id => e.Id})
                                       .ToArray());
        }
        
        /// <summary>
        /// Un-follow this user.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: Nothing.</para><para> </para>
        public static User Unfollow(this User e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Friendships.Destroy(Tokens,
                               (Parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]{user_id => e .Id})
                                       .ToArray());
        }
        
        /// <summary>
        /// Follow all of these users.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: Nothing.</para><para> </para>
        public static IEnumerable<User> FollowAll(this IEnumerable<User> e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            e.ForEach(x => x.Follow(Tokens, Parameters));
            return e;
        }
        
        /// <summary>
        /// Un-follow all of these users.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: Nothing.</para><para> </para>
        public static IEnumerable<User> UnfollowAll(this IEnumerable<User> e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            e.ForEach(x => x.Follow(Tokens, Parameters));
            return e;
        }
    }
}

