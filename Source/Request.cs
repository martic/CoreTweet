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
    public enum MethodType
    {
        GET,
        POST
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
        public static string Send(Tokens tokens, MethodType type, string url, params Expression<Func<string,string>>[] prms)
        {
            var prm = GenerateParameters(tokens.ConsumerKey, tokens.AccessToken);	
            foreach(var expr in prms)
                prm.Add(expr.Parameters[0].Name, UrlEncode(expr.Compile()("")));
            var sgn = GenerateSignature(tokens, 
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
                string.Join("&", prm.Select(x => string.Format("{0}={1}", x.Key, x.Value))));
            var res = req.GetResponse();
            using(var stream = res.GetResponseStream())
            using(var reader = new StreamReader(stream))
                return reader.ReadToEnd();

        }

        internal static string HttpPost(string url, IDictionary<string, string> prm)
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
            using(var resstr = req.GetResponse().GetResponseStream())
            using(var reader = new StreamReader(resstr, Encoding.UTF8))
                return reader.ReadToEnd();

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
}

