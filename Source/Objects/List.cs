using System;
using System.Linq;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet
{
    public class List : CoreBase
    { 
        public string Slug{ get; set; }

        public string Name{ get; set; }

        public DateTimeOffset CreatedAt{ get; set; }

        public Uri Uri{ get; set; }

        public int SubscriberCount { get; set; }

        public int MemberCount{ get; set; }

        public long Id{ get; set; }
        
        public string Mode{ get; set; }

        public string FullName{ get; set; }
        
        public string Description{ get; set; }

        public User User{ get; set; }

        public bool Following{ get; set; }
        
        public List(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Slug = e.slug;
            Name = e.name;
            //FIXME: DateTimeOffset.ParseExact Doesn't work.
            //CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
            //                                      System.Globalization.DateTimeFormatInfo.InvariantInfo);
            Uri = new Uri(e.uri);
            SubscriberCount = e.subsuriber_count;
            MemberCount = e.member_count;
            Id = (long)e.id;
            Mode = e.mode;
            FullName = e.full_name;
            Description = e.description;
            User = CoreBase.Convert<User>(this.Tokens, e.user);
            Following = e.following;
        }
    }
}
