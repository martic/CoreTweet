using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace TinyTweet
{
    public class Tokens
    {

        /// <summary>
        /// Gets or sets the consumer key.
        /// </summary>
        /// <value>
        /// The consumer key.
        /// </value>
        public string ConsumerKey 
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        /// <value>
        /// The consumer secret.
        /// </value>
        public string ConsumerSecret
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the access token secret.
        /// </summary>
        /// <value>
        /// The access token secret.
        /// </value>
        public string AccessTokenSecret
        {
            get;
            set;
        }
    }

    public enum MethodType
    {
        Get,
        Post
    }

    public class OAuthClient
    {
        static readonly string RequestTokenUrl = "https://twitter.com/oauth/request_token";
        static readonly string AccessTokenUrl = "https://twitter.com/oauth/access_token";
        static readonly string AuthorizeUrl = "https://twitter.com/oauth/authorize";
        string reqToken, reqSecret;
        Tokens token;

        public OAuthClient()
            : this(null)
        {
        }

        public OAuthClient(string consumerKey, string consumerSecret, string accessToken, string accessSecret)
            : this(new Tokens()
              {
                  ConsumerKey  = consumerKey,
                  ConsumerSecret = consumerSecret,
                  AccessToken = accessToken,
                  AccessTokenSecret = accessSecret,
              })
        {
        }

        public OAuthClient(Tokens t)
        {
            token = t;
        }

        /// <summary>
        ///     Generates the authorize URI.
        ///     Then call GetTokens(string) after get the pin code.
        /// </summary>
        /// <returns>
        ///     The authorize URI.
        /// </returns>
        /// <param name='consumerKey'>
        ///     Consumer key.
        /// </param>
        /// <param name='consumerSecret'>
        ///     Consumer secret.
        /// </param>
        public string GenerateAuthUri(string consumerKey, string consumerSecret)
        {
            var prm = GenerateParameters(consumerKey, null);
            var sgn = GenerateSignature(new Tokens()
            {
                ConsumerSecret = consumerSecret,
                AccessTokenSecret = null
            }, "GET", RequestTokenUrl, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            var dic = HttpGet(RequestTokenUrl, prm)
                .Split('&')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1]);
            reqToken = dic["oauth_token"];
            reqSecret = dic["oauth_token_secret"];
            token.ConsumerKey = consumerKey;
            token.ConsumerSecret = consumerSecret;
            return AuthorizeUrl + "?oauth_token=" + reqToken;
        }

        /// <summary>
        ///     Gets the OAuth tokens.
        ///     Be sure to call GenerateAuthUri(string,string) before call this.
        /// </summary>
        /// <param name='pin'>
        ///     Pin code.
        /// </param>
        /// <returns>
        ///     The tokens.
        /// </returns>
        public void GetTokens(string pin)
        {
            if(reqToken == null)
                throw new ArgumentNullException("req_token", "\"GenerateAuthUri\" haven't been called.");
            var prm = GenerateParameters(token.ConsumerKey, reqToken);
            prm.Add("oauth_verifier", pin);
            prm.Add("oauth_signature", GenerateSignature(new Tokens()
            {
                ConsumerSecret = reqSecret,
                AccessTokenSecret = null
            }, "GET", AccessTokenUrl, prm));
            var dic = HttpGet(AccessTokenUrl, prm)
                .Split('&')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1]);
            token = new Tokens()
            {
                ConsumerKey  = token.ConsumerKey ,
                ConsumerSecret = token.ConsumerSecret,
                AccessToken = dic["oauth_token"],
                AccessTokenSecret = dic["oauth_token_secret"],
            };
        }

        /// <summary>
        /// Send a request to Twitter.
        /// </summary>
        /// <param name='tokens'>
        /// OAuth Tokens.
        /// </param>
        /// <param name='type'>
        /// GET or POST.
        /// </param>
        /// <param name='url'>
        /// An URL of API.
        /// </param>
        /// <param name='prms'>
        /// Parameters.
        /// You can pass the parameters easily by writing lambda expressions.
        /// See the example.
        /// </param>
        /// <example>
        /// Request (tokens, MethodType.Post, "https://hoge.com/piyo.xml", prm1 => "hoge", prm2 => "piyo");
        /// </example>
        /// <returns>
        /// Response.
        /// </returns>
        public string Request(MethodType type, string url, params Expression<Func<string, string>>[] prms)
        {
            return Request(type, url, prms.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }

        public string Request(MethodType type, string url, IDictionary<string, string> prms)
        {
            var prm = GenerateParameters(token.ConsumerKey, token.AccessToken);
            foreach(var p in prms)
                prm.Add(p.Key, UrlEncode(p.Value));
            var sgn = GenerateSignature(token,
                type == MethodType.Get ? "GET" : "POST", url, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            return type == MethodType.Get ? HttpGet(url, prm) : HttpPost(url, prm);
        }

        static string HttpGet(string url, IDictionary<string, string> prm)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback
                  = (_, __, ___, ____) => true;
            var req = WebRequest.Create(url + '?' +
                string.Join("&", prm.Select(x => x.Key + "=" + x.Value))
            );
            var res = req.GetResponse();
            using(var stream = res.GetResponseStream())
            using(var reader = new StreamReader(stream))
                return reader.ReadToEnd();

        }

        static string HttpPost(string url, IDictionary<string, string> prm)
        {
            var data = Encoding.UTF8.GetBytes(
                string.Join("&", prm.Select(x => x.Key + "=" + x.Value)));
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback
                  = (_, __, ___, ____) => true;
            var req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            using(var reqstr = req.GetRequestStream())
                reqstr.Write(data, 0, data.Length);
            using(var resstr = req.GetResponse().GetResponseStream())
            using(var reader = new StreamReader(resstr, Encoding.UTF8))
                return reader.ReadToEnd();
        }

        static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
        {
            using(var hs1 = new HMACSHA1())
            {
                hs1.Key = Encoding.UTF8.GetBytes(
                    string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret),
                        t.AccessTokenSecret == null ? "" : UrlEncode(t.AccessTokenSecret))
                );
                var hash = hs1.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(
                        string.Format("{0}&{1}&{2}", httpMethod, UrlEncode(url),
                            UrlEncode(string.Join("&", prm.Select(x => string.Format("{0}={1}", x.Key, x.Value)))))
                )
                );
                return Convert.ToBase64String(hash);
            }
        }

        static SortedDictionary<string, string> GenerateParameters(string consumerKey, string token)
        {
            var ret = new SortedDictionary<string, string>()
            {
                {"oauth_consumer_key", consumerKey},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .TotalSeconds).ToString()},
                {"oauth_nonce", new Random().Next(int.MinValue, int.MaxValue).ToString("X")},
                {"oauth_version", "1.0"}
            };
            if(!string.IsNullOrEmpty(token))
                ret.Add("oauth_token", token);
            return ret;
        }

        static string UrlEncode(string text)
        {
            if(string.IsNullOrEmpty(text))
                return "";
            return string.Concat(Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".Contains((char)x) ?
                     ((char)x).ToString() : ('%' + x.ToString("X2"))));
        }
    }

    public static class Api
    {
        public static readonly string Version = "1.1";
        
        public static string Url(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Version, apiName);
        }
    }

    static class TestNow
    {
        static void Main()
        {
            var cnt = new OAuthClient();
            Console.WriteLine(cnt.GenerateAuthUri("consumer-key", "consumer-secret"));
            Console.Write("Input PIN: ");
            cnt.GetTokens(Console.ReadLine());
            // Get your Timeline. You can use a JSON library such as DynamicJson to parse this.
            Console.WriteLine(cnt.Request(MethodType.Get, Api.Url("statuses/home_timeline"), count => "20", page => "1"));
            // Your first tweet from TinyTweet!
            cnt.Request(MethodType.Post, Api.Url("statuses/update"), status => "Hello,Twitter!");
        }
    }
}