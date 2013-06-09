using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Alice.Extensions;

namespace CoreTweet.Ex
{
    /// <summary>
    /// Extensions for User object.
    /// </summary>
    public static class UserExtension
    {
        /// <summary>
        /// Follow this user.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool follow (optional)"/> : Enable notifications for the target user.</para>
        /// 
        public static User Follow(this User e, params Expression<Func<string,object>>[] parameters)
        {
            return e.Tokens.Friendships.Create((parameters as IEnumerable<Expression<Func<string,object>>>)
                                          .Union(new Expression<Func<string,object>>[]{user_id => e.Id})
                                          .ToArray());
        }
        
        /// <summary>
        /// Un-follow this user.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: Nothing.</para><para> </para>
        public static User Unfollow(this User e, params Expression<Func<string,object>>[] parameters)
        {
            return e.Tokens.Friendships.Destroy((parameters as IEnumerable<Expression<Func<string,object>>>)
                                          .Union(new Expression<Func<string,object>>[]{user_id => e .Id})
                                          .ToArray());
        }
        
        /// <summary>
        /// <para>Report the specified user as a spam account to Twitter. Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user./para>
        /// <para>Avaliable parameters: Nothing</para><para> </para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static User ReportForSpam(this User e)
        {
            return e.Tokens.Users.ReportSpam(user_id => e.Id);
        }
        
        
        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user. In addition the blocked user will not show in the authenticating users mentions or timeline (unless retweeted by another user). If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public static User Block(this User e, params Expression<Func<string,object>>[] parameters)
        {
            return e.Tokens.Blocks.Create((parameters as IEnumerable<Expression<Func<string,object>>>)
                                          .Union(new Expression<Func<string,object>>[]{user_id => e.Id})
                                          .ToArray());
        }
        
        
    }
}

