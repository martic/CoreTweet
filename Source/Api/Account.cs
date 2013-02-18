using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreTweet.Api
{
    public class Setting : CoreBase
    {
        public bool AlwaysUseHttps { get; set; }

        public bool DiscoverableByEmail{ get; set; }

        public bool GeoEnabled{ get; set; }

        public string Language{ get; set; }

        public bool Protected{ get; set; }

        public string ScreenName{ get; set; }

        public bool ShowAllInlineMedia{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the sleep time.</para>
        /// <para>Values: </para>
        /// <para>bool Enabled</para>
        /// <para>int EndTime</para>
        /// <para>int StartTime</para>
        /// </summary>
        public Dictionary<string,object> SleepTime{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the time zone.</para>
        /// <para>Values: </para>
        /// <para>string Name</para>
        /// <para>string TzinfoName</para>
        /// <para>int UtcOffset</para>
        /// </summary>
        public Dictionary<string,object> TimeZone{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the trend locaion.</para>
        /// <para>Values: </para>
        /// <para>string Country</para>
        /// <para>string CountryCode</para>
        /// <para>string Name</para>
        /// <para>long ParentId</para>
        /// <para>Dictionary(string,object) PlaceType [ int Code, string Name ]</para>
        /// <para>bool UseCookiePersonalization</para>
        /// </summary>
        public Dictionary<string,object> TrendLocaion{ get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            AlwaysUseHttps = e.always_use_https;
            DiscoverableByEmail = e.discoverable_by_email;
            GeoEnabled = e.geo_enabled;
            Language = e.language;
            Protected = e.@protected;
            ScreenName = e.screen_name;
            ShowAllInlineMedia = e.show_all_inline_media;
            SleepTime = new Dictionary<string, object>()
            {
                {"Enabled", e.sleep_time.enabled},
                {"EndTime", e.sleet_time.end_time},
                {"StartTime", e.sleet_time.start_time},
            };
            TimeZone = new Dictionary<string, object>()
            {
                {"Name", e.time_zone.name},
                
            };
        }
    }
        
    public static class Account
    {
        
        //GET Methods
        
        /// <summary>
        ///     <para>
        ///     Returns an HTTP 200 OK response code and a representation of the requesting user if authentication was successful; returns a 401 status code and an error message if not. Use this method to test if supplied user credentials are valid.
        ///     </para>
        ///     <para>
        ///     Available parameters:
        ///     </para>
        ///     <para>
        ///     bool include_entities (optional) : The entities node will not be included when set to false.
        ///     </para>
        ///     <para>
        ///     bool skip_status (optional) : When set to either true, t or 1 statuses will not be included in the returned user objects.
        ///     </para>
        /// </summary>
        /// <returns>
        /// The user data of you.
        /// </returns>
        /// <param name='tokens'>
        /// OAuth tokens.
        /// </param>
        /// <param name='Parameters'>
        /// Parameters.
        /// </param>
        public static User VerifyCredentials(Tokens tokens, params Expression<Func<string,object>>[] Parameters)
        {
            return CoreBase.Convert<User>(
                DynamicJson.Parse(
                    Request.Send(tokens, MethodType.GET, 
                         TwiTool.GetAPIURL("account/verify_credentials"), Parameters)));
        }
        
        //POST Methods
        
        
        
    }
}