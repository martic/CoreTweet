using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CSharp;

namespace CoreTweet
{
    public class Property
    {
        public static readonly string APIVersion = "1.1";
        public static readonly string CoreTweetVersion = "0.1";
    }

    public static class TwiTool
    {
        public static string GetAPIURL(string ApiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.APIVersion, ApiName);
        }
        
    }
}

