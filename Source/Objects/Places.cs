using System;
using System.Collections.Generic;

namespace CoreTweet
{
    public class Place : CoreBase
    {
		
		/// <summary>
		/// Contains a hash of variant information about the place. 
		/// </summary>
		/// <see cref="https://dev.twitter.com/docs/about-geo-place-attributes"/>
		public Dictionary<string,object> Attributes;
		
		/// <summary>
		/// A bounding box of coordinates which encloses this place.
		/// </summary>
		public BoundingBox BoundingBox;
		
		/// <summary>
		/// Name of the country containing this place.The country.
		/// </summary>
		public string Country;
		
		/// <summary>
		/// Shortened country code representing the country containing this place.
		/// </summary>
		public string CountryCode;
		
		/// <summary>
		/// Full human-readable representation of the place's name.
		/// </summary>
		public string FullName;
		
		/// <summary>
		/// ID representing this place. Note that this is represented as a string, not an integer.
		/// </summary>
		public string Id;
		
		/// <summary>
		/// Short human-readable representation of the place's name.
		/// </summary>
		public string Name;
		
		/// <summary>
		/// The type of location represented by this place.
		/// </summary>
		public string PlaceType;
		
		/// <summary>
		/// URL representing the location of additional place metadata for this place.
		/// </summary>
		public Uri Url;
		
        internal override void ConvertBase(dynamic e)
        {

        }
    }
    public class BoundingBox : CoreBase
    {
		/// <summary>
		/// A series of longitude and latitude points, defining a box which will contain the Place entity this bounding box is related to. Each point is an array in the form of [longitude, latitude]. Points are grouped into an array per bounding box. Bounding box arrays are wrapped in one additional array to be compatible with the polygon notation.
		/// </summary>
		public float[][][] Coordinates;
		
		/// <summary>
		/// The type of data encoded in the coordinates property. This will be "Polygon" for bounding boxes.
		/// </summary>
		public string Type;
		
        internal override void ConvertBase(dynamic e)
        {

        }
    }
}

