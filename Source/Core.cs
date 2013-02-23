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
        static readonly string REQUEST_TOKEN_URL = "https://twitter.com/oauth/request_token";
        static readonly string ACCESS_TOKEN_URL = "https://twitter.com/oauth/access_token";
        static readonly string AUTHORIZE_URL = "https://twitter.com/oauth/authorize";
        static string req_token, req_secret, c_key, c_secret;
        
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
        public static string GenerateAuthUri(string consumer_key, string consumer_secret)
        {
            var prm = Request.GenerateParameters(consumer_key, null);
            var sgn = Request.GenerateSignature(new Tokens()
                {ConsumerSecret = consumer_secret, AccessTokenSecret = null},
                    "GET", REQUEST_TOKEN_URL, prm);
            prm.Add("oauth_signature", Request.UrlEncode(sgn));
            var dic = Request.HttpGet(REQUEST_TOKEN_URL, prm).Split('&').Where(x => x.Contains('='))
                .ToDictionary(x => x.Substring(0, x.IndexOf('=')), y => y.Substring(y.IndexOf('=') + 1));
            req_token = dic["oauth_token"];
            req_secret = dic["oauth_token_secret"];
            c_key = consumer_key; 
            c_secret = consumer_secret;
            return AUTHORIZE_URL + "?oauth_token=" + req_token;
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
            if(req_token == null)
                throw new ArgumentNullException("req_token", "\"GenerateAuthUri\" haven't been called.");
            var prm = Request.GenerateParameters(c_key, req_token);
            prm.Add("oauth_verifier", pin);
            prm.Add("oauth_signature", Request.GenerateSignature(new Tokens()
                {ConsumerSecret = req_secret, AccessTokenSecret = null}, 
                    "GET", ACCESS_TOKEN_URL, prm));
            var dic = Request.HttpGet(ACCESS_TOKEN_URL, prm).Split('&').Where(x => x.Contains('='))
                .ToDictionary(x => x.Substring(0, x.IndexOf('=')), y => y.Substring(y.IndexOf('=') + 1));
            return Tokens.Create(c_key, c_secret, dic["oauth_token"], dic["oauth_token_secret"]);
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
        /// Send (tokens, MethodType.POST, "https://hoge.com/piyo.xml", prm1 => "hoge", prm2 => "piyo");
        /// </example>
        /// <returns>
        /// Response.
        /// </returns>
        public static string Send(Tokens tokens, MethodType type, string url, params Expression<Func<string,object>>[] prms)
        {
            var prm = GenerateParameters(tokens.ConsumerKey, tokens.AccessToken);    
            foreach(var expr in prms)
                prm.Add(expr.Parameters[0].Name, UrlEncode(expr.Compile()("").ToString()));
            var sgn = GenerateSignature(tokens, 
                type == MethodType.GET ? "GET" : "POST", url, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            return type == MethodType.GET ? HttpGet(url, prm) : 
                (type == MethodType.POST ? HttpPost(url, prm, false) : HttpPost(url, prm, true));
        }
        
        internal static string HttpGet(string url, IDictionary<string, string> prm)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback
                  = (_, __, ___, ____) => true;
            var req = WebRequest.Create(url + '?' + 
                string.Join("&", prm.Select(x => string.Format("{0}={1}", x.Key, x.Value))));
            var res = req.GetResponse();
            using(var stream = res.GetResponseStream())
            using(var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        internal static string HttpPost(string url, IDictionary<string, string> prm, bool ignore_response)
        {
            var data = Encoding.UTF8.GetBytes(
                string.Join("&", prm.Select(x => string.Format("{0}={1}", x.Key, x.Value))));
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback
                  = (_, __, ___, ____) => true;
            var req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            using(var reqstr = req.GetRequestStream())
                reqstr.Write(data, 0, data.Length);
            if(!ignore_response)
                using(var resstr = req.GetResponse().GetResponseStream())
                using(var reader = new StreamReader(resstr, Encoding.UTF8))
                    return reader.ReadToEnd();
            else
                return "";

        }
        
        internal static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
        {
            var hs1 = new HMACSHA1();
            hs1.Key = Encoding.UTF8.GetBytes(
                string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret), 
                    t.AccessTokenSecret == null ? "" : UrlEncode(t.AccessTokenSecret)));
            var hash = hs1.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(
                    string.Format("{0}&{1}&{2}", httpMethod, UrlEncode(url),
                        UrlEncode(string.Join("&", prm.Select(x => string.Format("{0}={1}", x.Key, x.Value)))))));
            return Convert.ToBase64String(hash);
        }
        
        internal static SortedDictionary<string, string> GenerateParameters(string ConsumerKey, string token)
        {
            var ret = new SortedDictionary<string, string>()
            {
                {"oauth_consumer_key", ConsumerKey},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0))
                    .TotalSeconds.ToString().Split(new []{'.'})[0]},
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
                return String.Empty;
            return string.Join("", Encoding.UTF8.GetBytes(text)
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

