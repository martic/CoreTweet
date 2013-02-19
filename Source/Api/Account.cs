using System;
using Codeplex.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreTweet
{
    public class Setting : CoreBase
    {
        /// <summary>
        /// When true, always use https.
        /// </summary>
        /// <value>
        /// <c>true</c> if always use https; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysUseHttps { get; set; }

        /// <summary>
        /// When true, your friend can discover you by your email address.
        /// </summary>
        public bool DiscoverableByEmail{ get; set; }

        /// <summary>
        ///     When true, indicates that the user has enabled the possibility of geotagging their Tweets. This field must be true for the current user to attach geographic data when using POST statuses/update.
        /// </summary>
        public bool GeoEnabled{ get; set; }

        /// <summary>
        ///     The BCP 47 code for the user's self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        public string Language{ get; set; }
  
        /// <summary>
        ///     When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        public bool Protected{ get; set; }
  
        /// <summary>
        ///     The screen name, handle, or alias that this user identifies themselves with. screen_names are unique but subject to change. Use id_str as a user identifier whenever possible. Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.
        /// </summary>
        public string ScreenName{ get; set; }
  
        /// <summary>
        ///     Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        public bool? ShowAllInlineMedia{ get; set; }
        
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
        /// </summary>
        public Dictionary<string,object> TrendLocaion{ get; set; }
        
        public bool UseCookiePersonalization { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            AlwaysUseHttps = e.always_use_https;
            DiscoverableByEmail = e.discoverable_by_email;
            GeoEnabled = e.geo_enabled;
            Language = e.language;
            Protected = e.@protected;
            ScreenName = e.screen_name;
            ShowAllInlineMedia = e.IsDefined("show_all_inline_media") ? (bool?)e.show_all_inline_media : null;
            SleepTime = new Dictionary<string, object>()
            {
                {"Enabled", e.sleep_time.enabled},
                {"EndTime", e.sleep_time.enabled ? e.sleet_time.end_time : -1},
                {"StartTime", e.sleep_time.enabled ? e.sleet_time.start_time : -1},
            };
            TimeZone = new Dictionary<string, object>()
            {
                {"Name", e.time_zone.name},
                {"TzinfoName", e.time_zone.tzinfo_name},
                {"UtcOffset", e.time_zone.utc_offset}
            };
            TrendLocaion = new Dictionary<string, object>()
            {
                {"Country", e.IsDefined("trend_location.country") ? e.trend_location.country : null},
                {"CountryCode", e.IsDefined("trend_location.country_code") ? e.trend_location.country_code : null},
                {"Name", e.IsDefined("trend_location.country_code") ? e.trend_location.name : null},
                {"ParentId", e.IsDefined("trend_location.parent_id") ? e.trend_location.parent_id : null},
                {"PlaceType",e.IsDefined("trend_location.place_type") ? new Dictionary<string,object>(){ {"Code",e.trend_location.place_type.code},{"Name",e.trend_location.place_type.name} } : null},
            };
            UseCookiePersonalization = e.use_cookie_personalization;
        }
    }
        
    public static partial class Api
    {
        public static class Account
        {
        
            //GET Methods
        
            /// <summary>
            ///     <para>
            ///     Returns an HTTP 200 OK response code and a representation of the requesting user if authentication was successful; returns a 401 status code and an error message if not. Use this method to test if supplied user credentials are valid.
            ///     </para>
            ///     <para>
            ///     Available parameters:
            ///     </para><para> </para>
            ///     <para>
            ///     <paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.
            ///     </para>
            ///     <para>
            ///     <paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.
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
            public static User VerifyCredentials(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(
                DynamicJson.Parse(
                    Request.Send(Tokens, MethodType.GET, 
                         TwiTool.GetAPIURL("account/verify_credentials"), Parameters)));
            }
        
            //GET & POST Methods
        
            /// <summary>
            /// <para>Returns settings (including current trend, geo and sleep time information) for the authenticating user or updates the authenticating user's settings.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="int trend_location_woeid (optional)"/> : The Yahoo! Where On Earth ID to use as the user's default trend location. Global information is available by using 1 as the WOEID. The woeid must be one of the locations returned by GET trends/available.</para>
            /// <para><paramref name="bool sleep_time_enabled (optional)"/> : When set to true, will enable sleep time for the user. Sleep time is the time when push or SMS notifications should not be sent to the user.</para>
            /// <para><paramref name="int start_sleep_time (optional)"/> : The hour that sleep time should begin if it is enabled. The value for this parameter should be provided in ISO8601 format (i.e. 00-23). The time is considered to be in the same timezone as the user's time_zone setting.</para>
            /// <para><paramref name="int end_sleep_time (optional)"/> : The hour that sleep time should end if it is enabled. The value for this parameter should be provided in ISO8601 format (i.e. 00-23). The time is considered to be in the same timezone as the user's time_zone setting.</para>
            /// <para><paramref name="string time_zone (optional)"/> : The timezone dates and times should be displayed in for the user. The timezone must be one of the Rails TimeZone names.</para>
            /// <para><paramref name="string lang (optional)"/> : The language which Twitter should render in for this user. The language must be specified by the appropriate two letter ISO 639-1 representation. Currently supported languages are provided by GET help/languages.</para>
            /// </summary>
            /// <param name='Tokens'>
            /// OAuth Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static Setting Settings(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<Setting>(
                DynamicJson.Parse(
                    Request.Send(Tokens, (Parameters.Length == 0 ? MethodType.GET : MethodType.POST),
                             TwiTool.GetAPIURL("account/settings"), Parameters)));
            }
        
            //POST Methods
        
            /// <summary>
            /// <para>Sets which device Twitter delivers updates to for the authenticating user. Sending none as the device parameter will disable SMS updates.</para>
            /// <para>Avaliable parameters: </para>
            /// <para><paramref name="string device (required)"/> : Must be one of: sms, none.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : When set to true, each tweet will include a node called "entities,". This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. While entities are opt-in on timelines at present, they will be made a default component of output in the future. See Tweet Entities for more detail on entities.</para>
            /// </summary>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static void UpdateDeliveryService(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_delivery_device"), Parameters);
            }
        
            /// <summary>
            /// <para>Sets values that users are able to set under the "Account" tab of their settings page. Only the parameters specified will be updated.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string name (optional)"/> : Full name associated with the profile. Maximum of 20 characters.</para>
            /// <para><paramref name="string url (optional)"/> : URL associated with the profile. Will be prepended with "http://" if not present. Maximum of 100 characters.</para>
            /// <para><paramref name="string location (optional)"/> : The city or country describing where the user of the account is located. The contents are not normalized or geocoded in any way. Maximum of 30 characters.</para>
            /// <para><paramref name="string description (optional)"/> : A description of the user owning the account. Maximum of 160 characters.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional) "/>: When set to true, statuses will not be included in the returned user objects.</para>
            /// </summary>
            /// <returns>
            /// The profile.
            /// </returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User UpdateProfile(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(
                DynamicJson.Parse(
            Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_profile"), Parameters)));
            }
        
            /// <summary>
            /// <para>Updates the authenticating user's profile background image. This method can also be used to enable or disable the profile background image.</para>
            /// <para>Although each parameter is marked as optional, at least one of image, tile or use must be provided when making this request.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="object image (optional)"/> : The background image for the profile, base64-encoded. Must be a valid GIF, JPG, or PNG image of less than 800 kilobytes in size. Images with width larger than 2048 pixels will be forcibly scaled down. The image must be provided as raw multipart data, not a URL.</para>
            /// <para><paramref name="bool tile (optional)"/> : Whether or not to tile the background image. If set to true, t or 1 the background image will be displayed tiled. The image will not be tiled otherwise.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
            /// <para><paramref name="bool use (optional)"/> : Determines whether to display the profile background image or not. When set to true, the background image will be displayed if an image is being uploaded with the request, or has been uploaded previously. An error will be returned if you try to use a background image when one is not being uploaded or does not exist. If this parameter is defined but set to anything other than true, the background image will stop being used.</para>
            /// </summary>
            /// <returns>The profile.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User UpdateProfileBackgroundImage(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(
                DynamicJson.Parse(
            Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_profile_background_image"), Parameters)));
            }
        
            /// <summary>
            /// <para>Uploads a profile banner on behalf of the authenticating user. For best results, upload an image that is exactly 1252px by 626px and smaller than 5MB. Images will be resized for a number of display options. Users with an uploaded profile banner will have a profile_banner_url node in their Users objects. More information about sizing variations can be found in User Profile Images and Banners and GET users/profile_banner.</para>
            /// <para>Profile banner images are processed asynchronously. The profile_banner_url and its variant sizes will not necessary be available directly after upload.</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="object banner (required)"/> : The Base64-encoded or raw image data being uploaded as the user's new profile banner.</para>
            /// <para><paramref name="string width (optional)"/> : The width of the preferred section of the image being uploaded in pixels. Use with height, offset_left, and offset_top to select the desired region of the image to use.</para>
            /// <para><paramref name="string height (optional)"/> : The height of the preferred section of the image being uploaded in pixels. Use with width, offset_left, and offset_top to select the desired region of the image to use.</para>
            /// <para><paramref name="string offset_left (optional)"/> : The number of pixels by which to offset the uploaded image from the left. Use with height, width, and offset_top to select the desired region of the image to use.</para>
            /// <para><paramref name="string offset_top (optional)"/> : The number of pixels by which to offset the uploaded image from the top. Use with height, width, and offset_left to select the desired region of the image to use.</para>
            /// </summary>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static void UpdateProfileBanner(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_profile_banner"), Parameters);
            }
        
            /// <summary>
            /// <para>Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com. Each parameter's value must be a valid hexidecimal value, and may be either three or six characters (ex: #fff or #ffffff).</para>
            /// <para>Avaliable parameters: </para><para> </para>
            /// <para><paramref name="string profile_background_color (optional)"/> : Profile background color.</para>
            /// <para><paramref name="string profile_link_color (optional)"/> : Profile link color.</para>
            /// <para><paramref name="string profile_sidebar_border_color (optional)"/> : Profile sidebar's border color.</para>
            /// <para><paramref name="string profile_sidebar_fill_color (optional)"/> : Profile sidebar's background color.</para>
            /// <para><paramref name="string profile_text_color (optional)"/> : Profile text color.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
            /// </summary>
            /// <returns>The profile.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User UpdateProfileColors(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(
                DynamicJson.Parse(
            Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_profile_colors"), Parameters))); 
            }
        
            /// <summary>
            /// <para>Updates the authenticating user's profile image. Note that this method expects raw multipart data, not a URL to an image.</para>
            /// <para>This method asynchronously processes the uploaded file before updating the user's profile image URL. You can either update your local cache the next time you request the user's information, or, at least 5 seconds after uploading the image, ask for the updated URL using GET users/show.</para>
            /// <para>Avaliable parameters: </para><para> </para> 
            /// <para><paramref name="object image (required)"/> : The avatar image for the profile, base64-encoded. Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size. Images with width larger than 500 pixels will be scaled down. Animated GIFs will be converted to a static GIF of the first frame, removing the animation.</para>
            /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
            /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
            /// </summary>
            /// <returns>The profile.</returns>
            /// <param name='Tokens'>
            /// Tokens.
            /// </param>
            /// <param name='Parameters'>
            /// Parameters.
            /// </param>
            public static User UpdateProfileImage(Tokens Tokens, params Expression<Func<string,object>>[] Parameters)
            {
                return CoreBase.Convert<User>(
                DynamicJson.Parse(
                     Request.Send(Tokens, MethodType.POST, TwiTool.GetAPIURL("account/update_profile_image"), Parameters)));
            }
        }
        
    }
}
