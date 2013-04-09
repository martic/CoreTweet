using System;
using System.Net;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Embed : CoreBase
    {   
        public string Html { get; set; }

        public string AuthorName { get; set; }
        
        public string AuthorUrl { get; set; }

        public string ProviderUrl{ get; set; }
  
        public string ProviderName{ get; set; }
        
        public Uri Url{ get; set; }

        public string Version { get; set; }

        public string Type{ get; set; }

        public int? Height{ get; set; }

        public int? Width{ get; set; }
        
        public string CacheAge{ get; set; }
        
        internal Embed(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Html = e.html;
            AuthorName = e.author_name;
            AuthorUrl = e.author_url;
            ProviderName = e.provider_name;
            ProviderUrl = e.provider_url;
            Url = new Uri(e.url);
            Version = e.version;
            Type = e.type;
            Height = (int?)e.height;
            Width = (int?)e.Width;
            CacheAge = e.cache_age;
        }
    }
}

