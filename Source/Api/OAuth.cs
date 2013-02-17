using System;
using System.Linq;

namespace CoreTweet
{
	/*public class Tokens
    {
        public string ConsumerKey {get;set;}
        public string ConsumerSecret {get;set;}
        public string AccessToken {get;set;}
        public string AccessTokenSecret {get;set;}
        public static Tokens Create(string consumer_key,string consumer_secret,string access_token,string access_secret)
        {
            return new Tokens()
            {
                ConsumerKey = consumer_key,
                ConsumerSecret = consumer_secret,
                AccessToken = access_token,
                AccessTokenSecret = access_secret
            };
        }
    }*/
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
		public static string GenerateAuthUri (string consumer_key, string consumer_secret)
		{
			var prm = Request.GenerateParameters (consumer_key, null);
			var sgn = Request.GenerateSignature (new Tokens ()
                {ConsumerSecret = consumer_secret, AccessTokenSecret = null},
                    "GET", REQUEST_TOKEN_URL, prm);
			prm.Add ("oauth_signature", Request.UrlEncode (sgn));
			var dic = Request.HttpGet (REQUEST_TOKEN_URL, prm).Split ('&').Where (x => x.Contains ('='))
                .ToDictionary (x => x.Substring (0, x.IndexOf ('=')), y => y.Substring (y.IndexOf ('=') + 1));
			req_token = dic ["oauth_token"];
			req_secret = dic ["oauth_token_secret"];
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
		public static Tokens GetTokens (string pin)
		{
			if (req_token == null)
				throw new ArgumentNullException ("req_token", "\"GenerateAuthUri\" haven't been called.");
			var prm = Request.GenerateParameters (c_key, req_token);
			prm.Add ("oauth_verifier", pin);
			prm.Add ("oauth_signature", Request.GenerateSignature (new Tokens ()
                {ConsumerSecret = req_secret, AccessTokenSecret = null}, 
                    "GET", ACCESS_TOKEN_URL, prm));
			var dic = Request.HttpGet (ACCESS_TOKEN_URL, prm).Split ('&').Where (x => x.Contains ('='))
                .ToDictionary (x => x.Substring (0, x.IndexOf ('=')), y => y.Substring (y.IndexOf ('=') + 1));
			return Tokens.Create (c_key, c_secret, dic ["oauth_token"], dic ["oauth_token_secret"]);
		}

	}
}

