using System;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Place : CoreBase
    {
        
        /// <summary>
        /// Contains a hash of variant information about the place. 
        /// </summary>
        /// <see cref="https://dev.twitter.com/docs/about-geo-place-attributes"/>
        public Dictionary<string,object> Attributes { get; set; }
        
        /// <summary>
        /// A bounding box of coordinates which encloses this place.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }
        
        public Place[] ContainedWithin { get; set; }
        
        /// <summary>
        /// Name of the country containing this place.The country.
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Shortened country code representing the country containing this place.
        /// </summary>
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Full human-readable representation of the place's name.
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// ID representing this place. Note that this is represented as a string, not an integer.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Short human-readable representation of the place's name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type of location represented by this place.
        /// </summary>
        public string PlaceType { get; set; }
        
        /// <summary>
        /// URL representing the location of additional place metadata for this place.
        /// </summary>
        public Uri Url { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {

        }
    }

    public class BoundingBox : CoreBase
    {
        /// <summary>
        /// A series of longitude and latitude points, defining a box which will contain the Place entity this bounding box is related to. Each point is an array in the form of [longitude, latitude]. Points are grouped into an array per bounding box. Bounding box arrays are wrapped in one additional array to be compatible with the polygon notation.
        /// </summary>
        public float[][][] Coordinates { get; set; }
        
        /// <summary>
        /// The type of data encoded in the coordinates property. This will be "Polygon" for bounding boxes.
        /// </summary>
        public string Type { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Coordinates = e.coordinates as float[][][];
            Type = e.type;
        }
        
    }
    
    public class GeoResult : CoreBase, IEnumerable<Place>
    {
        /// <summary>
        /// Places.
        /// </summary>
        public Place[] Places { get; set; }

        /// <summary>
        /// The token needed to be able to create a new place.
        /// </summary>
        public string Token { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Places = CoreBase.ConvertArray<Place>(e.places);
            Token = e.IsDefined("token") ? e.token : null;
        }
        
        public System.Collections.IEnumerator GetEnumerator()
        {
            return Places.GetEnumerator();
        }
        
        IEnumerator<Place> IEnumerable<Place>.GetEnumerator()
        {
            return (Places as IEnumerable<Place>).GetEnumerator();
        }
    }
}

