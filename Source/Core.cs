using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CoreTweet
{   
    public static class OAuth
    {
        static readonly string RequestTokenUrl = "https://twitter.com/oauth/request_token";
        static readonly string AccessTokenUrl = "https://twitter.com/oauth/access_token";
        static readonly string AuthorizeUrl = "https://twitter.com/oauth/authorize";
        static string reqToken, reqSecret, cKey, cSecret;
        
        /// <summary>
        ///     Generates the authorize URI.
        ///     Then call GetTokens(string) after get the pin code.
        /// </summary>
        /// <returns>
        ///     The authorize URI.
        /// </returns>
        /// <param name='consumer_key'>
        ///     Consumer key.
        /// </param>
        /// <param name='consumer_secret'>
        ///     Consumer secret.
        /// </param>
        public static string GenerateAuthUri(string consumerKey, string consumerSecret)
        {
            var prm = Request.GenerateParameters(consumerKey, null);
            var sgn = Request.GenerateSignature(new Tokens()
            {
                ConsumerSecret = consumerSecret,
                AccessTokenSecret = null
            }, "GET", RequestTokenUrl, prm);
            prm.Add("oauth_signature", Request.UrlEncode(sgn));
            var dic = Request.HttpGet(RequestTokenUrl, prm)
                .Split('&')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1]);
            reqToken = dic["oauth_token"];
            reqSecret = dic["oauth_token_secret"];
            cKey = consumerKey;
            cSecret = consumerSecret;
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
        public static Tokens GetTokens(string pin)
        {
            if(reqToken == null)
                throw new ArgumentNullException("req_token", "\"GenerateAuthUri\" haven't been called.");
            var prm = Request.GenerateParameters(cKey, reqToken);
            prm.Add("oauth_verifier", pin);
            prm.Add("oauth_signature", Request.GenerateSignature(new Tokens()
            {
                ConsumerSecret = reqSecret,
                AccessTokenSecret = null
            }, "GET", AccessTokenUrl, prm));
            var dic = Request.HttpGet(AccessTokenUrl, prm)
                .Split('&')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1]);
            reqToken = reqSecret = null;
            return Tokens.Create(cKey, cSecret, dic["oauth_token"], dic["oauth_token_secret"]);
        }

    }

    public enum MethodType
    {
        GET,
        POST,
        POST_NORESPONSE
    }
    
    public static class Request
    {
             
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
        /// Request (tokens, MethodType.POST, "https://hoge.com/piyo.xml", prm1 => "hoge", prm2 => "piyo");
        /// </example>
        /// <returns>
        /// Response.
        /// </returns>
        public static string Send(Tokens token, MethodType type, string url, params Expression<Func<string, object>>[] prms)
        {
            return Send(token, type, url, prms.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }

        public static string Send(Tokens token, MethodType type, string url, IDictionary<string, object> prms)
        {
            var prm = GenerateParameters(token.ConsumerKey, token.AccessToken);
            foreach(var p in prms)
                prm.Add(p.Key, UrlEncode(p.Value.ToString()));
            var sgn = GenerateSignature(token,
                type == MethodType.GET ? "GET" : "POST", url, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            return type == MethodType.GET ? HttpGet(url, prm) : HttpPost(url, prm);
        }

        internal static string HttpGet(string url, IDictionary<string, string> prm)
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

        internal static string HttpPost(string url, IDictionary<string, string> prm)
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

        internal static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
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

        internal static SortedDictionary<string, string> GenerateParameters(string ConsumerKey, string token)
        {
            var ret = new SortedDictionary<string, string>()
            {
                {"oauth_consumer_key", ConsumerKey},
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

        public static string UrlEncode(string text)
        {
            if(string.IsNullOrEmpty(text))
                return "";
            return string.Concat(Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".Contains((char)x) ?
                     ((char)x).ToString() : ('%' + x.ToString("X2"))));
        }
    }
    
    public class Property
    {
        /// <summary>
        /// The version of the Twitter API.
        /// Changing this value is not recommended. 
        /// </summary>
        public static string ApiVersion = "1.1";
        /// <summary>
        /// The version of CoreTweet.
        /// </summary>
        public static readonly string CoreTweetVersion = "0.1";
        /// <summary>
        /// The authors.
        /// </summary>
        public static readonly string[] Authors = {"canno", "karno"};
        /// <summary>
        /// The license of CoreTweet.
        /// </summary>
        public static readonly string License = "Microsoft Public License (Ms-PL)";
        /// <summary>
        /// The URL you can get helps about CoreTweet.
        /// </summary>
        public static readonly string HelpUrl = "https://twitter.com/a1cn";
    }

    public static partial class Rest
    {
        /// <summary>
        /// Gets the url of the specified api's name.
        /// </summary>
        /// <returns>The url.</returns>
        public static string Url(string ApiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, ApiName);
        }
        
        
    }
}

