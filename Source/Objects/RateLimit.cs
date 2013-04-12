using System;
using CoreTweet;
using CoreTweet.Core;

namespace CoreTweet
{
    public class RateLimit : CoreBase
    {
        internal RateLimit(Tokens tokens) : base(tokens) { }
        
        public int Remaining { get; set; }
        
        public DateTime Reset { get; set; }
        
        public int Limit { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Remaining = (int)e.remaining;
            Reset = new DateTime((long)e.reset);
            Limit = (int)e.limit;
        }
    }
}

