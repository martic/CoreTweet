using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet.Core
{
    /// <summary>
    /// The token included class.
    /// </summary>
    public abstract class TokenIncluded
    {
        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>
        /// The tokens.
        /// </value>
        internal Tokens Tokens { get; set; }
        
        public TokenIncluded() : this(null) { }
        
        public TokenIncluded(Tokens tokens)
        {
            Tokens = tokens;
        }
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
        /// <summary>
        /// Rest/Account
        /// </summary>
        public Account Account { get { return new Account(this); } }
        /// <summary>
        /// Rest/Blocks
        /// </summary>
        public Blocks Blocks { get { return new Blocks(this); } }
        /// <summary>
        /// Rest/Direct messages.
        /// </summary>
        public DirectMessages DirectMessages { get { return new DirectMessages(this); } }
        /// <summary>
        /// Rest/Favorites.
        /// </summary>
        public Favorites Favorites { get { return new Favorites(this); } }
        /// <summary>
        /// Rest/Friends.
        /// </summary>
        public Friends Friends { get { return new Friends(this); } }
        /// <summary>
        /// Rest/Friendships.
        /// </summary>
        public Friendships Friendships { get { return new Friendships(this); } }
        /// <summary>
        /// Rest/Geo.
        /// </summary>
        public Geo Geo { get { return new Geo(this); } }
        /// <summary>
        /// Rest/Help.
        /// </summary>
        public Help Help { get { return new Help(this); } }
        /// <summary>
        /// Rest/Lists.
        /// </summary>
        public Lists Lists { get { return new Lists(this); } }
        /// <summary>
        /// Rest/Search.
        /// </summary>
        public Search Search { get { return new Search(this); } }
        /// <summary>
        /// Rest/Saved searches.
        /// </summary>
        public SavedSearches SavedSearches { get { return new SavedSearches(this); } }
        /// <summary>
        /// Rest/Statuses.
        /// </summary>
        public Statuses Statuses { get { return new Statuses(this); } }
        /// <summary>
        /// Rest/Trends.
        /// </summary>
        public Trends Trends { get { return new Trends(this); } }
        /// <summary>
        /// Rest/Users.
        /// </summary>
        public Users Users { get { return new Users(this); } }
        
        public Tokens() { }
        
        public Tokens(Tokens e) : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.AccessToken = e.AccessToken;
            this.AccessTokenSecret = e.AccessTokenSecret;
        }
        
        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Stream SendRequest(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
        {
            return this.SendRequest(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }
        
        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Stream SendRequest(MethodType type, string url, IDictionary<string,object> parameters)
        {
            var prms = Request.GenerateParameters(this.ConsumerKey, this.AccessToken);
            foreach(var p in parameters)
                prms.Add(p.Key, Request.UrlEncode(p.Value.ToString()));
            var sgn = Request.GenerateSignature(this,
                type == MethodType.Get ? "GET" : "POST", url, prms);
            prms.Add("oauth_signature", Request.UrlEncode(sgn));
            return type == MethodType.Get ? Request.HttpGet(url, prms) : Request.HttpPost(url, prms);
        }
        
        internal T AccessApi<T>(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
            where T : CoreBase
        {
            return this.AccessApi<T>(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }
        
        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters)
            where T : CoreBase
        {
            dynamic d;
            using(var s = this.SendRequest(type, Url(url), parameters))
            using(var sr = new StreamReader(s))
                d = DynamicJson.Parse(sr.ReadToEnd());
            return CoreBase.Convert<T>(this, d);
        }
        
        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
            where T : CoreBase
        {
            return this.AccessApiArray<T>(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }
        
        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string,object> parameters)
            where T : CoreBase
        {
            dynamic d;
            using(var s = this.SendRequest(type, Url(url), parameters))
            using(var sr = new StreamReader(s))
                d = DynamicJson.Parse(sr.ReadToEnd());
            return CoreBase.ConvertArray<T>(this, d);
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
        internal static string Url(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }
    }
}
