using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Standalone OAuth Twitter Module.
/// </summary>
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
    
    public enum MethodType
    {
        GET,
        POST
    }

    public class OAuthClient
    {
        static readonly string REQUEST_TOKEN_URL = "https://twitter.com/oauth/request_token";
        static readonly string ACCESS_TOKEN_URL = "https://twitter.com/oauth/access_token";
        static readonly string AUTHORIZE_URL = "https://twitter.com/oauth/authorize";
        string req_token, req_secret;
        Tokens Token;
        
        public OAuthClient()
        {
        }
        
        public OAuthClient(string Consumer_Key, string Consumer_Secret, string Access_Token, string Acccess_Secret)
        {
            Token = Tokens.Create(Consumer_Key, Consumer_Secret, Access_Token, Acccess_Secret);
        }
        
        public OAuthClient(Tokens t)
        {
            Token = t;
        }

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
        public string GenerateAuthUri(string consumer_key, string consumer_secret)
        {
            var prm = GenerateParameters(consumer_key, null);
            var sgn = GenerateSignature(new Tokens()
                {ConsumerSecret = consumer_secret, AccessTokenSecret = null},
                    "GET", REQUEST_TOKEN_URL, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            var dic = HttpGet(REQUEST_TOKEN_URL, prm).Split('&').Where(x => x.Contains('='))
                .ToDictionary(x => x.Substring(0, x.IndexOf('=')), y => y.Substring(y.IndexOf('=') + 1));
            req_token = dic["oauth_token"];
            req_secret = dic["oauth_token_secret"];
            Token.ConsumerKey = consumer_key; 
            Token.ConsumerSecret = consumer_secret;
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
        public void GetTokens(string pin)
        {
            if(req_token == null)
                throw new ArgumentNullException("req_token", "\"GenerateAuthUri\" haven't been called.");
            var prm = GenerateParameters(Token.ConsumerKey, req_token);
            prm.Add("oauth_verifier", pin);
            prm.Add("oauth_signature", GenerateSignature(new Tokens()
                {ConsumerSecret = req_secret, AccessTokenSecret = null}, 
                    "GET", ACCESS_TOKEN_URL, prm));
            var dic = HttpGet(ACCESS_TOKEN_URL, prm).Split('&').Where(x => x.Contains('='))
                .ToDictionary(x => x.Substring(0, x.IndexOf('=')), y => y.Substring(y.IndexOf('=') + 1));
            Token = Tokens.Create(Token.ConsumerKey, Token.ConsumerSecret, dic["oauth_token"], dic["oauth_token_secret"]);
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
        /// Request (tokens, MethodType.POST, "https://hoge.com/piyo.xml", prm1 => "hoge", prm2 => "piyo");
        /// </example>
        /// <returns>
        /// Response.
        /// </returns>
        public string Request(MethodType type, string url, params Expression<Func<string,string>>[] prms)
        {
            var prm = GenerateParameters(Token.ConsumerKey, Token.AccessToken);    
            foreach(var expr in prms)
                prm.Add(expr.Parameters[0].Name, UrlEncode(expr.Compile()("")));
            var sgn = GenerateSignature(Token, 
                type == MethodType.GET ? "GET" : "POST", url, prm);
            prm.Add("oauth_signature", UrlEncode(sgn));
            return type == MethodType.GET ? HttpGet(url, prm) : HttpPost(url, prm);
        }
        
        static string HttpGet(string url, IDictionary<string, string> prm)
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

        static string HttpPost(string url, IDictionary<string, string> prm)
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
    
        static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
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
        
        static SortedDictionary<string, string> GenerateParameters(string ConsumerKey, string token)
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

        static string UrlEncode(string text)
        {   
            if(string.IsNullOrEmpty(text))
                return String.Empty;
            return string.Join("", Encoding.UTF8.GetBytes(text)
             .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".Contains((char)x) ?
                     ((char)x).ToString() : ('%' + x.ToString("X2"))));
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
            Console.WriteLine(cnt.Request(MethodType.GET, "https://api.twitter.com/1.1/statuses/home_timeline.json", count => "20", page => "1"));
            // Your first tweet from TinyTweet!
            cnt.Request(MethodType.POST, "https://api.twitter.com/1.1/statuses/update.json", status => "Hello,Twitter!");
        }
    }
    
}

