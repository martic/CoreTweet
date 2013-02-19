using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CSharp;

namespace CoreTweet
{
    public class Property
    {
        public static readonly string API_Version = "1.1";
    }

    public static class TwiTool
    {
        public static string GetAPIURL(string ApiName)
        {
            return string.Format("https://api.twitter.com/1.1/{0}.json", ApiName);
        }
    }
}

