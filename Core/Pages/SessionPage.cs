using Xamarin.Forms;

namespace ThatConfXamarin
{
	public class SessionPage : TabbedPage
	{
		public SessionPage ()
		{
			this.Children.Add (new DayTemplatePage (1)); 
			this.Children.Add (new DayTemplatePage (2)); 
			this.Children.Add (new DayTemplatePage (3)); 
			this.Children.Add (new DayTemplatePage (4)); 
			this.Children.Add (new DayTemplatePage (5)); 
		}
	}
}