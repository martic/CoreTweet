using System;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Tokens
    {
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
    }
}

