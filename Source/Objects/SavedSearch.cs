using System;
using System.Linq;
using CoreTweet.Core;

namespace CoreTweet
{
    public class SavedSearch : CoreBase
    {
        public DateTimeOffset CreatedAt{ get; set; }

        public long Id{ get; set; }

        public string Name{ get; set; }

        public string Query{ get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            //FIXME: DateTimeOffset.ParseExact Doesn't work.
            //CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
            //                                      System.Globalization.DateTimeFormatInfo.InvariantInfo);
            Id = (long)e.id;
            Name = e.name;
            Query = e.query;
        }
    }
}

