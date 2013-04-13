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
using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Streaming;

namespace CoreTweet.Streaming.Rx
{
    public class StreamingApiRx : StreamingApi
    {
        internal StreamingApiRx(Tokens tokens) :  base(tokens) { }
        
        public new IObservable<string> StartUserStream()
        {
            return StartUserStream(new StreamingParameters());
        }
        
        public new IObservable<string> StartUserStream(StreamingParameters parameters)
        {
            var cntr = this.Connect(new StreamingParameters(stall_warnings => true), MethodType.Get)
                .ToObservable(Scheduler.ThreadPool)
                .Publish();
            cntr.Connect();
            return cntr;
        }
    }

    public static class RxExtension
    {
        public static StreamingApiRx StreamingRx(this Tokens tokens)
        {
            return new StreamingApiRx(tokens);
        }
    }
}

