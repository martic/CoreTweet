using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.PlatformServices;
using Codeplex.Data;
using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Streaming;

namespace CoreTweet.Streaming.Reactive
{
    public class StreamingApiReactive : StreamingApi
    {
        protected internal StreamingApiReactive(Tokens tokens) : base(tokens) { }
        
        public IObservable<StreamingMessage> StartUserStream(StreamingParameters parameters,
                                                             StreamingType type)
        {
            return ReactiveBase(parameters, type).ToObservable(Scheduler.ThreadPool);
        }
        
        IEnumerable<StreamingMessage> ReactiveBase(StreamingParameters parameters, StreamingType type)
        {
            var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" : 
                          type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
                          type == StreamingType.Public ? "https://stream.twitter.com/1.1/statuses/filter.json" : "";
                
            var str = this.Connect(parameters, type == StreamingType.Public ? MethodType.Post : MethodType.Get, url)
                          .Where(x => !string.IsNullOrEmpty(x));
            
            foreach(var s in str)
            {
                yield return CoreBase.Convert<RawJsonMessage>(this.Tokens, s);
                yield return StreamingMessage.Parse(this.Tokens, DynamicJson.Parse(s));
            }
        }
        
    }
    
    public static class Extension
    {
        public static StreamingApiReactive StreamingReactive(this Tokens e)
        {
            return new StreamingApiReactive(e);
        }
    }
    
}

