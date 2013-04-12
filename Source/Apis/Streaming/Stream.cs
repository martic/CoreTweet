using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CoreTweet;
using CoreTweet.Core;

namespace CoreTweet.Streaming
{
    public class Streaming : TokenIncluded
    {
        internal Streaming(Tokens tokens) : base(tokens) { }
        
        public void User()
        {
            
        }
        
        
        
    }
    
    /// <summary>
    /// Parameters for streaming API.
    /// </summary>
    public class StreamingParameters
    {
        IDictionary<string,object> prms;
        
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.</para>
        /// <para>Avaliable parameters: </para>
        /// <para>*Note: In filter stream, at least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para><paramref name="bool stall_warnings (optional)"/> : Specifies whether stall warnings should be delivered.</para>
        /// <para><paramref name="string follow (optional, required in site stream, ignored in user stream)"/> : A comma separated list of user IDs, indicating the users to return statuses for in the stream. </para>
        /// <para><paramref name="string track (optional*)"/> : Keywords to track. Phrases of keywords are specified by a comma-separated list. </para>
        /// <para><paramref name="string location (optional*)"/> : A comma-separated list of longitude,latitude pairs specifying a set of bounding boxes to filter Tweets by. example: "-74,40,-73,41" </para>
        /// <para><paramref name="string with (optional)"/> : Specifies whether to return information for just the authenticating user (with => "user"), or include messages from accounts the user follows (with => "followings").</para>
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public StreamingParameters(params Expression<Func<string,object>>[] streamingParameters)
         : this(streamingParameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()(""))) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        public StreamingParameters(IDictionary<string,object> streamingParameters)
        {
            prms = streamingParameters;
        }
    }
}

