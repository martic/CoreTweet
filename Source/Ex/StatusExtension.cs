using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Alice;

namespace CoreTweet.Ex
{
    /// <summary>
    /// Extensions for Status object.
    /// </summary>
    public static class StatusExtension
    {
        /// <summary>
        /// Reply to this tweet.
        /// </summary>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="double lat (optional)"/> : The latitude of the location this tweet refers to. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
        /// <para><paramref name="double long (optional)"/> : The longitude of the location this tweet refers to. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
        /// <para><paramref name="string place_id (optional)"/> : A place in the world. These IDs can be retrieved from GET geo/reverse_geocode.</para>
        /// <para><paramref name="bool display_coordinates (optional)"/> : Whether or not to put a pin on the exact coordinates a tweet has been sent from.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static Status ReplyToThis(this Status e, Func<Status,string> Text, params Expression<Func<string,object>>[] parameters)
        {
            return new Tokens(e).Statuses.Update(
                               (parameters as IEnumerable<Expression<Func<string,object>>>)
                                   .Union(new Expression<Func<string,object>>[]
                                       {status => Text(e), in_reply_to_status_id => e.InReplyToStatusId})
                                           .ToArray());
        }
        
        /// <summary>
        /// Retweet this tweet.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static Status Retweet(this Status e, params Expression<Func<string,object>>[] parameters)
        {
            return new Tokens(e).Statuses.Retweet(
                       (parameters as IEnumerable<Expression<Func<string,object>>>)
                           .Union(new Expression<Func<string,object>>[]{id => e.Id})
                               .ToArray());
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
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static Status Destroy(this Status e, params Expression<Func<string,object>>[] parameters)
        {
            return new Tokens(e).Statuses.Destroy(
                       (parameters as IEnumerable<Expression<Func<string,object>>>)
                           .Union(new Expression<Func<string,object>>[]{id => e.Id})
                               .ToArray());
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
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static Status Favorite(this Status e, params Expression<Func<string,object>>[] parameters)
        {
            return new Tokens(e).Favorites.Create(
                       (parameters as IEnumerable<Expression<Func<string,object>>>)
                           .Union(new Expression<Func<string,object>>[]{id => e.Id})
                               .ToArray());
            
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
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static Status Unfavorite(this Status e, params Expression<Func<string,object>>[] parameters)
        {
            return new Tokens(e).Favorites.Destroy(
                       (parameters as IEnumerable<Expression<Func<string,object>>>)
                           .Union(new Expression<Func<string,object>>[]{id => e.Id})
                               .ToArray());
        }
        
        /// <summary>
        /// Reply to all of these tweets.
        /// </summary>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="double lat (optional)"/> : The latitude of the location this tweet refers to. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
        /// <para><paramref name="double long (optional)"/> : The longitude of the location this tweet refers to. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
        /// <para><paramref name="string place_id (optional)"/> : A place in the world. These IDs can be retrieved from GET geo/reverse_geocode.</para>
        /// <para><paramref name="bool display_coordinates (optional)"/> : Whether or not to put a pin on the exact coordinates a tweet has been sent from.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static IEnumerable<Status> ReplyToAll(this IEnumerable<Status> e, Func<Status,string> Text, params Expression<Func<string,object>>[] parameters)
        {
            e.ForEach(x => x.ReplyToThis(Text, parameters));
            return e;
        }
        
        /// <summary>
        /// Retweet all of these tweets.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static IEnumerable<Status> RetweetAll(this IEnumerable<Status> e, params Expression<Func<string,object>>[] parameters)
        {
            e.ForEach(x => x.Retweet(parameters));
            return e;
        }
        
        /// <summary>
        /// Destroy all of these tweets.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        public static IEnumerable<Status> DestroyAll(this IEnumerable<Status> e, params Expression<Func<string,object>>[] parameters)
        {
            e.ForEach(x => x.Destroy(parameters));
            return e;
        }
        
        /// <summary>
        /// Favorite all of these tweets.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static IEnumerable<Status> FavoriteAll(this IEnumerable<Status> e, params Expression<Func<string,object>>[] parameters)
        {
            e.ForEach(x => x.Favorite(parameters));
            return e;
        }
        
        /// <summary>
        /// Un-favorite all of these tweets.
        /// </summary>
        /// <param name='Tokens'>
        /// Tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        public static IEnumerable<Status> UnfavoriteAll(this IEnumerable<Status> e, params Expression<Func<string,object>>[] parameters)
        {
            e.ForEach(x => x.Unfavorite(parameters));
            return e;
        }
    }
}

