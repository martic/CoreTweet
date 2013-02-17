using System;
using System.Linq;

namespace CoreTweet
{
    public class Entity : CoreBase
    {
        /// <summary>
        ///     Represents hashtags which have been parsed out of the Tweet text.
        /// </summary>
        public HashTag[] HashTags;

        /// <summary>
        ///     Represents media elements uploaded with the Tweet.
        /// </summary>
        public Media Media;

        /// <summary>
        ///     Represents URLs included in the text of a Tweet or within textual fields of a user object.
        /// </summary>
        public Url[] Urls;

        /// <summary>
        ///     Represents other Twitter users mentioned in the text of the Tweet.
        /// </summary>
        public UserMention[] UserMentions;

        internal override void ConvertBase(dynamic e)
        {
			HashTags = CoreBase.ConvertArray<HashTag>(e.hash_tags);
			Media = CoreBase.Convert<Media>(e.media);
			Urls = CoreBase.ConvertArray<Url>(e.urls);
			UserMentions = CoreBase.ConvertArray<UserMention>(e.user_mention);
        }
    }

    public class HashTag : CoreBase
    {
        /// <summary>
        ///     Name of the hashtag, minus the leading '#' character.
        /// </summary>
        public string Text;

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the hashtag begins and ends. The first integer represents the location of the # character in the Tweet text string. The second integer represents the location of the first character after the hashtag. Therefore the difference between the two numbers will be the length of the hashtag name plus one (for the '#' character).
        /// </summary>
        public int[] Indices { get; set; }

        internal override void ConvertBase(dynamic e)
        {
            Indices = new[] { (int)e.indices[0], (int)e.indices[1] };
            Text = (string)e.text;
        }
    }

    public class Media : CoreBase
    {
        /// <summary>
        ///     URL of the media to display to clients.
        /// </summary>
        public string DisplayUrl;

        /// <summary>
        ///     An expanded version of display_url. Links to the media display page.
        /// </summary>
        public string ExpandedUrl;

        /// <summary>
        ///     ID of the media expressed as a 64-bit integer.
        /// </summary>
        public long Id;

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character occurring after the URL (or the end of the string if the URL is the last part of the Tweet text).
        /// </summary>
        public int[] Indices;

        /// <summary>
        ///     An http:// URL pointing directly to the uploaded media file.
        /// </summary>
        public Uri MediaUrl;

        /// <summary>
        ///     An https:// URL pointing directly to the uploaded media file, for embedding on https pages.
        /// </summary>
        public Uri MediaUrlHttps;

        /// <summary>
        ///     An object showing available sizes for the media file.
        /// </summary>
        public Sizes Sizes;

        /// <summary>
        ///     For Tweets containing media that was originally associated with a different tweet, this ID points to the original Tweet.
        /// </summary>
        public long SourceStatusId;

        /// <summary>
        ///     Type of uploaded media.
        /// </summary>
        public string Type;

        /// <summary>
        ///     Wrapped URL for the media link. This corresponds with the URL embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        public Uri Url;

        internal override void ConvertBase(dynamic e)
        {
			DisplayUrl = e.display_url;
			ExpandedUrl = e.expanded_url;
			Id = e.id;
			Indices = new int[]{e.indices[0],e.indices[1]};
			MediaUrl = new Uri(e.media_url);
			MediaUrlHttps = new Uri(e.media_url_https);
			Sizes = CoreBase.Convert<Sizes>(e.sizes);
			SourceStatusId = e.source_status_id;
			Type = e.type;
			Url = new Uri(e.url);
        }
    }

    public class Size : CoreBase
    {
        /// <summary>
        ///     Height in pixels of this size.
        /// </summary>
        public int Height;

        /// <summary>
        ///     Resizing method used to obtain this size. A value of fit means that the media was resized to fit one dimension, keeping its native aspect ratio. A value of crop means that the media was cropped in order to fit a specific resolution.
        /// </summary>
        public string Resize;

        /// <summary>
        ///     Width in pixels of this size.
        /// </summary>
        public int Width;

        internal override void ConvertBase(dynamic e)
        {
			Height = e.height;
			Resize = e.resize;
			Width = e.width;
        }
    }

    public class Sizes : CoreBase
    {
        /// <summary>
        ///     Information for a large-sized version of the media.
        /// </summary>
        public Size Large;

        /// <summary>
        ///     Information for a medium-sized version of the media.
        /// </summary>
        public Size Medium;

        /// <summary>
        ///     Information for a small-sized version of the media.
        /// </summary>
        public Size Small;

        /// <summary>
        ///     Information for a thumbnail-sized version of the media.
        /// </summary>
        public Size Thumb;

        internal override void ConvertBase(dynamic e)
		{
			Large  = CoreBase.Convert<Size>(e.large);
			Medium = CoreBase.Convert<Size>(e.medium);
			Small = CoreBase.Convert<Size>(e.small);
			Thumb = CoreBase.Convert<Size>(e.thumb);
        }
    }

    public class Url : CoreBase
    {
        /// <summary>
        ///     Version of the URL to display to clients.
        /// </summary>
        public string DisplayUrl;

        /// <summary>
        ///     Expanded version of display_url.
        /// </summary>
        public string ExpandedUrl;

        /// <summary>
        ///     An array of integers representing offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character after the end of the URL.
        /// </summary>
        public int[] Indices;

        /// <summary>
        ///     Wrapped URL, corresponding to the value embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        public Uri Uri;

        internal override void ConvertBase(dynamic e)
        {
			DisplayUrl = e.display_url;
			ExpandedUrl = e.expanded_url;
			Indices = new int[]{e.indices[0],e.indices[1]};
			Uri = new Uri(e.uri);
        }
    }

    public class UserMention : CoreBase
    {
        /// <summary>
        ///     ID of the mentioned user, as an integer.
        /// </summary>
        public long Id;

        /// <summary>
        ///     An array of integers representing the offsets within the Tweet text where the user reference begins and ends. The first integer represents the location of the '@' character of the user mention. The second integer represents the location of the first non-screenname character following the user mention.
        /// </summary>
        public int[] Indices;

        /// <summary>
        ///     Display name of the referenced user.
        /// </summary>
        public string Name;

        /// <summary>
        ///     Screen name of the referenced user.
        /// </summary>
        public string ScreenName;

        internal override void ConvertBase(dynamic e)
        {
			Id = e.id;
			Indices = new int[]{e.indices[0],e.indices[1]};
			Name = e.name;
			ScreenName = e.screen_name;
        }
    }
}