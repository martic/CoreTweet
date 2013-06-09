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

    public static class Extension
    {
		/// <summary>
		/// Starts the stream.
		/// </summary>
		/// <returns>The observable object. Connect() must be called.</returns>
		/// <param name="e">Tokens.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="type">Type of streaming API.</param>
		public static IObservable<StreamingMessage> StartObservableStream(this StreamingApi e, StreamingParameters parameters,
		                                                     StreamingType type)
		{
			return ReactiveBase(e, parameters, type).ToObservable().Publish();
		}

		static IEnumerable<string> Connect(Tokens e,StreamingParameters parameters, MethodType type, string url)
		{
			using(var str = e.SendRequest(type, url, parameters.Parameters))
				using(var reader = new StreamReader(str))
					foreach(var s in reader.EnumerateLines()
					        .Where(x => !string.IsNullOrEmpty(x)))
						yield return s;
		}

		
		static IEnumerable<StreamingMessage> ReactiveBase(this StreamingApi e, StreamingParameters parameters, StreamingType type)
		{
			var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" : 
				type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
					type == StreamingType.Public ? "https://stream.twitter.com/1.1/statuses/filter.json" : "";
			
			var str = Connect(e.IncludedTokens, parameters, type == StreamingType.Public ? MethodType.Post : MethodType.Get, url)
				       .Where(x => !string.IsNullOrEmpty(x));
			
			foreach(var s in str)
			{
				yield return CoreBase.Convert<RawJsonMessage>(e.IncludedTokens, s);
				yield return StreamingMessage.Parse(e.IncludedTokens, DynamicJson.Parse(s));
			}
		}

	    static IEnumerable<string> EnumerateLines(this StreamReader streamReader)
		{
			while(!streamReader.EndOfStream)
				yield return streamReader.ReadLine();
		}
    }
    
}

