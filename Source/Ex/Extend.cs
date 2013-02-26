using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Ex
{
    public static class StatusExtension
    {
        /// <summary>
        /// Retweet this tweet.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static Status Retweet(this Status e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Statuses.Retweet(Tokens, 
                       (Expression<Func<string,object>>[])Parameters.Concat(
                           new Expression<Func<string,object>>[]{id => e.Id}));
        }
        
        /// <summary>
        /// Destroy this tweet.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static Status Destroy(this Status e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Statuses.Destroy(Tokens, 
                       (Expression<Func<string,object>>[])Parameters.Concat(
                           new Expression<Func<string,object>>[]{id => e.Id}));
        }
        
        /// <summary>
        /// Favorite this tweet.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static Status Favor(this Status e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Favorites.Create(Tokens, 
                       (Expression<Func<string,object>>[])Parameters.Concat(
                           new Expression<Func<string,object>>[]{id => e.Id}));
        }
        
        /// <summary>
        /// Un-favorite this tweet.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static Status Unfavor(this Status e, Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return Rest.Favorites.Create(Tokens, 
                       (Expression<Func<string,object>>[])Parameters.Concat(
                           new Expression<Func<string,object>>[]{id => e.Id}));
        }
    }

    internal class DeveloperExtention
    {
       
    }
}

