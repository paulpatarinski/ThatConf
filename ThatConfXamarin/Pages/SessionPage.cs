using Xamarin.Forms;

namespace ThatConfXamarin
{
	public class SessionPage : TabbedPage
	{
		public SessionPage ()
		{
			this.Children.Add (new DayPageTemplate (1)); 
			this.Children.Add (new DayPageTemplate (2)); 
			this.Children.Add (new DayPageTemplate (3)); 
			this.Children.Add (new DayPageTemplate (4)); 
			this.Children.Add (new DayPageTemplate (5)); 
		}
	}
}