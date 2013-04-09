using System;
using System.Linq;
using CoreTweet.Core;

namespace CoreTweet
{
    public class SearchQuery : CoreBase
    {
        public DateTimeOffset CreatedAt{ get; set; }

        public long? Id{ get; set; }

        public string Name{ get; set; }

        public string Query{ get; set; }
        
        internal SearchQuery(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            //FIXME: DateTimeOffset.ParseExact Doesn't work.
            //CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
            //                                      System.Globalization.DateTimeFormatInfo.InvariantInfo);
            Id = e.IsDefined("id") ? (long?)e.id : null;
            Name = e.name;
            Query = e.query;
        }
    }
}

