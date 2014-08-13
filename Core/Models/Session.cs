using System;
using Newtonsoft.Json;

namespace Core.Models
{

	public class Rootobject
	{
		[JsonProperty ("Property1")]
		public Session[] Sessions { get; set; }
	}


	public class Session
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Category { get; set; }

		public string Level { get; set; }

		public DateTime ScheduledDateTime { get; set; }

		public string ScheduledRoom { get; set; }

		public bool IsFamilyApproved { get; set; }

		public object IsUserFavorite { get; set; }

		public bool Accepted { get; set; }

		public bool Canceled { get; set; }

		public Speaker[] Speakers { get; set; }

		public Tag[] Tags { get; set; }

		public Sessionlink[] SessionLinks { get; set; }

		public DateTime LastUpdated { get; set; }

		public bool Updated { get; set; }

		public bool ShowMoreDetails { get; set; }
	}

	public class Speaker
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string HeadShot { get; set; }

		public string UserName { get; set; }

		public string Biography { get; set; }

		public string WebSite { get; set; }

		public string Company { get; set; }

		public string Title { get; set; }

		public string Twitter { get; set; }

		public string Facebook { get; set; }

		public string GooglePlus { get; set; }

		public string LinkedIn { get; set; }

		public string GitHub { get; set; }

		public DateTime LastUpdated { get; set; }
	}

	public class Tag
	{
		public string Name { get; set; }
	}

	public class Sessionlink
	{
		public string LinkDescription { get; set; }

		public string LinkUrl { get; set; }
	}

}

