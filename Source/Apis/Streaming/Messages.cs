using System;
using CoreTweet;
using CoreTweet.Core;

namespace CoreTweet.Streaming
{
	public enum DisconnectCode
	{
		Shutdown,
		DuplicateStream,
		ControlRequest,
		Stall,
		Normal,
		TokenRevoked,
		AdminLogout,
		Reserved,
		MaxMessageLimit,
		StreamException,
		BrokerStall,
		ShedLoad
	}

	public enum EventCode
	{
		Block,
		Unblock,
		Favorite,
		Unfavorite,
		Follow,
		Unfollow,
		ListCreated,
		ListDestroyed,
		ListUpdated,
		ListMemberAdded,
		ListMemberRemoved,
		ListUserSubscribed,
		ListUserUnsubscribed,
		UserUpdate
	}

	public abstract class StreamingMessage : CoreBase
	{
		internal StreamingMessage () : base(null)
		{
		}
	}

	public class IDMessage : StreamingMessage
	{
		public long Id { get; set; }
	
		public long UserId { get; set; }
	
		public long UpToStatusId { get; set; }
	
		public string[] WithheldInCountries { get; set; }
	
		internal override void ConvertBase (dynamic e)
		{
		
		}
	}

	public class DisconnectMessage : StreamingMessage
	{
		public DisconnectCode Code { get; set; }
	
		public string StreamName { get; set; }
	
		public string Reason { get; set; }
	
		internal override void ConvertBase (dynamic e)
		{
		
		}
	}

	public class WarningMessage : StreamingMessage
	{
		public string Code { get; set; }

		public string Message { get; set; }

		public int PercentFull { get; set; }

		internal override void ConvertBase (dynamic e)
		{

		}
	}

	public class EventMessage<T> : StreamingMessage
		where T : CoreBase
	{


		public User Target { get; set; }

		public User Source { get; set; }

		public EventCode Event { get; set; }

		public T TargetObject { get; set; }

		public DateTime CreatedAt { get; set; }

		internal override void ConvertBase (dynamic e)
		{

		}
	}
}

