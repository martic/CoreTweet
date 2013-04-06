using System;
using CoreTweet.Core;

namespace CoreTweet.Core
{
    public class _Tokens
    {
        internal Tokens _ { get { return Tokens.Create(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret); } }
        
        /// <summary>
        /// The consumer key.
        /// </summary>
        public string ConsumerKey { get; internal set; }
        /// <summary>
        /// The consumer secret.
        /// </summary>
        public string ConsumerSecret { get; internal set; }
        /// <summary>
        /// The access token.
        /// </summary>
        public string AccessToken { get; internal set; }
        /// <summary>
        /// The access token secret.
        /// </summary> 
        public string AccessTokenSecret { get; internal set; }
    }
}

namespace CoreTweet
{
    /// <summary>
    /// The OAuth tokens.
    /// </summary>
    public class Tokens : _Tokens
    {
        internal Tokens() { }
        
        public Account Account { get { return (this as _Tokens) as Account; } }
        
        public Blocks Blocks { get { return (this as _Tokens) as Blocks; } }
        
        public DirectMessages DirectMessages { get { return (this as _Tokens) as DirectMessages; } }
        
        public Favorites Favorites { get { return (this as _Tokens) as Favorites; } }
        
        public Friends Friends { get { return (this as _Tokens) as Friends; } }
        
        public Friendships Friendships { get { return (this as _Tokens) as Friendships; } }
        
        public Geo Geo { get { return (this as _Tokens) as Geo; } }
        
        public Help Help { get { return (this as _Tokens) as Help; } }
        
        public Search Search { get { return (this as _Tokens) as Search; } }
        
        public SavedSearches SavedSearches { get { return (this as _Tokens) as SavedSearches; } }
        
        public Statuses Statuses { get { return (this as _Tokens) as Statuses; } }
        
        public Trends Trends { get { return (this as _Tokens) as Trends; } }

        public Users Users { get { return (this as _Tokens) as Users; } }

        public Lists Lists { get { return (this as _Tokens) as Lists; } }
        
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
                ConsumerKey = consumerKey,
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
