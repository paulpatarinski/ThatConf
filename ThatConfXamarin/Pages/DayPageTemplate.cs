using System;
using Xamarin.Forms;

namespace ThatConfXamarin
{
	public class DayPageTemplate : ContentPage
	{
		public DayPageTemplate (int day)
		{
			Title = "Day " + day;
			var viewModel = new SessionViewModel (new ThatConfService (new System.Net.Http.HttpClient ()));
			BindingContext = viewModel;
			viewModel.LoadSessionsAsync (day);

			var stackLayout = new StackLayout ();

			var sessionListView = new ListView ();

			var sessionItemTemplate = new DataTemplate (typeof(ImageCell));

			sessionItemTemplate.SetBinding (ImageCell.TextProperty, "ItemTemplateTextProperty");
			sessionItemTemplate.SetValue (ImageCell.TextColorProperty, Color.FromHex ("#3498DB"));
			sessionItemTemplate.SetBinding (ImageCell.DetailProperty, "ItemTemplateDetailProperty");
			sessionItemTemplate.SetValue (ImageCell.DetailColorProperty, Color.White);
//			sessionItemTemplate.SetBinding (ImageCell.ImageSourceProperty, "ItemTemplateIconProperty");


			sessionListView.ItemTemplate = sessionItemTemplate;
			sessionListView.BindingContext = viewModel;
			sessionListView.SetBinding<SessionViewModel> (ListView.ItemsSourceProperty, vm => vm.SessionList);

			var slider = new Slider ();

			//From 5 am to 10 pm
			slider.Maximum = 22.0;
			slider.Minimum = 5.0;
			slider.SetBinding<SessionViewModel> (Slider.ValueProperty, vm => vm.Hour, BindingMode.TwoWay);

			stackLayout.Children.Add (sessionListView);
			stackLayout.Children.Add (slider);

			Content = stackLayout;
		}
	}
}

