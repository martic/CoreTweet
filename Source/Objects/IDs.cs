using System;

namespace CoreTweet
{
	public class IDs : CoreBase
	{
		public long[] Ids{ get; set; }
		public long NextCursor{ get; set; }
		public long PreviousCursor{ get; set; }

		internal override void ConvertBase (dynamic e)
		{
			Ids = (long[])e.ids;
			NextCursor = (long)e.next_cursor;
			PreviousCursor = (long)e.previous_cursor;
		}
	}
}

