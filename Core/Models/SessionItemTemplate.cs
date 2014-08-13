using System;

namespace Core.Models
{
	public class SessionItemTemplate
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public DateTime SessionDate { get; set; }

		public TimeSpan SessionTime { get; set; }
	}
}

