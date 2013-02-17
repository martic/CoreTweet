using System;

namespace CoreTweet
{
    public class Property
    {
        public static readonly string API_Version = "1.1";
    }

    public static class ApiList
    {
        public static class Account
        {
            public static string
                verify_credentials = "https://api.twitter.com/1.1/account/verify_credentials.json",
                settings = "https://api.twitter.com/1.1/account/settings.json";
        }
    }
}

