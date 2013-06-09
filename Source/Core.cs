using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CoreTweet.Core;
using Alice.Extensions;

namespace CoreTweet
{   
    /// <summary>
    /// The type of the HTTP method.
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// GET method.
        /// </summary>
        Get,
        /// <summary>
        /// POST method.
        /// </summary>
        Post,
        /// <summary>
        /// POST method without any response.
        /// </summary>
        PostNoResponse
    }
    
    public static class OAuth
    {
        /// <summary>
        /// The request token URL.
        /// </summary>
        static readonly string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        /// <summary>
        /// The access token URL.
        /// </summary>
        static readonly string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";
        /// <summary>
        /// The authorize URL.
        /// </summary>
        static readonly string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        /// <summary>
        /// The tmp values.
        /// </summary>
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
            Dictionary<string,string> dic;
            using(var str = Request.HttpGet(RequestTokenUrl, prm))
            using(var reader = new StreamReader(str))
                dic = reader.ReadToEnd()
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
                throw new ArgumentNullException("reqToken", "\"GenerateAuthUri\" haven't been called.");
            var prm = Request.GenerateParameters(cKey, reqToken);
            prm.Add("oauth_verifier", pin);
            prm.Add("oauth_signature", Request.GenerateSignature(
                Tokens.Create(null, cSecret, null, null), "GET", AccessTokenUrl, prm));
            var dic = new Dictionary<string,string>();
            using(var str = Request.HttpGet(AccessTokenUrl, prm))
            using(var reader = new StreamReader(str))      
                dic = reader.ReadToEnd()
                .Split('&')
                .Where(x => x.Contains('='))
                .Select(x => x.Split('='))
                .ToDictionary(x => x[0], y => y[1]);
            return Tokens.Create(cKey, cSecret, dic["oauth_token"], dic["oauth_token_secret"]);
        }

    }

    /// <summary>
    /// Sends a request to Twitter and some other web services.
    /// </summary>
    internal static class Request
    {  
        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        internal static Stream HttpGet(string url, IDictionary<string, string> prm)
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback
                  = (_, __, ___, ____) => true;
            var req = WebRequest.Create(url + '?' +
                string.Join("&", prm.Select(x => x.Key + "=" + x.Value))
            );
            return req.GetResponse().GetResponseStream();
        }

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="response">If it set false, won't try to get any responses and will return null.</param>
        internal static Stream HttpPost(string url, IDictionary<string, string> prm, bool response)
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
            return response ? req.GetResponse().GetResponseStream() : null;
        }
        

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <returns>The signature.</returns>
        /// <param name="t">Tokens.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="url">the URL.</param>
        /// <param name="prm">Parameters.</param>
        internal static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
        {
            using(var hs1 = new HMACSHA1())
            {
                hs1.Key = Encoding.UTF8.GetBytes(
                    string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret),
                        UrlEncode(t.AccessTokenSecret) ?? ""));
                var hash = hs1.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(
                        string.Format("{0}&{1}&{2}", httpMethod, UrlEncode(url),
                            UrlEncode(prm.Select(x => string.Format("{0}={1}", x.Key, x.Value))
                                         .JoinToString("&")))));
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Generates the parameters.
        /// </summary>
        /// <returns>The parameters.</returns>
        /// <param name="ConsumerKey ">Consumer key.</param>
        /// <param name="token">Token.</param>
        internal static SortedDictionary<string, string> GenerateParameters(string consumerKey, string token)
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

        /// <summary>
        /// Encodes the specified text.
        /// </summary>
        /// <returns>The encoded text.</returns>
        /// <param name="text">Text.</param>
        internal static string UrlEncode(string text)
        {
            if(string.IsNullOrEmpty(text))
                return null;
            return Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"
                        .Contains((char)x) ? ((char)x).ToString() : ('%' + x.ToString("X2")))
                .JoinToString();
        }
    }
}

