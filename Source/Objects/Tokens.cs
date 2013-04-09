using System;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet.Core
{
    public abstract class TokenIncluded
    {
        internal TokenIncluded(Tokens tokens)
        {
            Tokens = tokens;
        }
        
        internal Tokens Tokens { get; set; }
    }
}

namespace CoreTweet
{
    /// <summary>
    /// The OAuth tokens.
    /// </summary>
    public class Tokens
    {
        /// <summary>
        /// The consumer key.
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// The consumer secret.
        /// </summary>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// The access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// The access token secret.
        /// </summary> 
        public string AccessTokenSecret { get; set; }
        
        public Account Account { get { return new Account(this); } }
        
        public Blocks Blocks { get { return new Blocks(this); } }
        
        public DirectMessages DirectMessages { get { return new DirectMessages(this); } }
        
        public Favorites Favorites { get { return new Favorites(this); } }
        
        public Friends Friends { get { return new Friends(this); } }
        
        public Friendships Friendships { get { return new Friendships(this); } }
        
        public Geo Geo { get { return new Geo(this); } }
        
        public Help Help { get { return new Help(this); } }
        
        public Search Search { get { return new Search(this); } }
        
        public SavedSearches SavedSearches { get { return new SavedSearches(this); } }
        
        public Statuses Statuses { get { return new Statuses(this); } }
        
        public Trends Trends { get { return new Trends(this); } }

        public Users Users { get { return new Users(this); } }

        public Lists Lists { get { return new Lists(this); } }
        
        internal Tokens() { }
        
        internal Tokens(Tokens e) : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.AccessToken = e.AccessToken;
            this.AccessTokenSecret = e.AccessTokenSecret;
        }
        
        /// <summary>
        /// Make an instance of Tokens.
        /// </summary>
        /// <param name='consumer_key'>
        /// Consumer key.
        /// </param>
        /// <param name='consumer_secret'>
        /// Consumer secret.
        /// </param>
        /// <param name='access_token'>
        /// Access token.
        /// </param>
        /// <param name='access_secret'>
        /// Access secret.
        /// </param>
        public static Tokens Create(string consumerKey, string consumerSecret, string accessToken, string accessSecret)
        {
            return new Tokens()
            {
                ConsumerKey  = consumerKey,
                ConsumerSecret = consumerSecret,
                AccessToken = accessToken,
                AccessTokenSecret = accessSecret
            };
        }
        
        /// <summary>
        /// Gets the url of the specified api's name.
        /// </summary>
        /// <returns>The url.</returns>
        public static string Url(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }
    }
}
