using System;
using System.Linq;
using Codeplex.Data;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Configurations : CoreBase
    {
        public int CharactersReservedPerMedia { get; set; }

        public int MaxMediaPerUpload{ get; set; }
        
        public string[] NonUsernamePaths{ get; set; }
        
        public int PhotoSizeLimit{ get; set; }
        
        public int ShortUrlLength{ get; set; }
        
        public int ShortUrlLengthHttps{ get; set; }
        
        public Sizes PhotoSizes{ get; set; }
        
        internal Configurations(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            CharactersReservedPerMedia = e.characters_reserved_per_media;
            MaxMediaPerUpload = e.max_media_per_upload;
            NonUsernamePaths = e.non_username_paths;
            PhotoSizeLimit = e.photo_size_limit;
            ShortUrlLength = e.short_url_length;
            ShortUrlLengthHttps = e.short_url_length_https;
            PhotoSizes = CoreBase.Convert<Sizes>(this.Tokens, e.photo_sizes);
        }
    }
    
    public class Language : CoreBase
    {
        public string Code{ get; set; }

        public string Name{ get; set; }

        public string Status{ get; set; }
        
        internal Language(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Code = e.code;
            Name = e.name;
            Status = e.status;
        }
    }
}

