using System;

namespace ThatConfXamarin
{
	public class SesssionItemTemplate
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public DateTime SessionDate { get; set; }

		public TimeSpan SessionTime { get; set; }

		public string ItemTemplateTextProperty { get { return Title; } }

		public string ItemTemplateDetailProperty { get { return Description; } }

		public string ItemTemplateIconProperty { get { return ImageUrl; } }
	}
}

