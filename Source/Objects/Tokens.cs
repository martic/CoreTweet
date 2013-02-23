using System;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Tokens
    {
        /// <summary>
<<<<<<< HEAD
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
=======
        /// Gets or sets the consumer key.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }
        /// <summary>
        /// Gets or sets the access token secret.
        /// </summary>
        /// <value>
        /// The access token secret.
        /// </value>
>>>>>>> 3dea60089054de7b357a6dbc20cdc397dbe901e0
        public string AccessTokenSecret { get; set; }
		
        /// <summary>
        /// An useful method to make an instance of Tokens :)
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
        public static Tokens Create(string consumer_key, string consumer_secret, string access_token, string access_secret)
        {
            return new Tokens()
			{
				ConsumerKey = consumer_key,
				ConsumerSecret = consumer_secret,
				AccessToken = access_token,
				AccessTokenSecret = access_secret
			};
        }
    }
}

