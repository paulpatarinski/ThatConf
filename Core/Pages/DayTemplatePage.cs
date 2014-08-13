using System;
using Xamarin.Forms;

namespace ThatConfXamarin
{
	public class DayTemplatePage : ContentPage
	{
	  private SessionViewModel _viewModel;
	  private int _day;

	  public DayTemplatePage (int day)
		{
	    _day = day;
	    BackgroundColor = Color.FromHex ("ecf0f1");
			Title = "Day " + day;

			_viewModel = new SessionViewModel (new ThatConfService (new System.Net.Http.HttpClient ()));
      BindingContext = _viewModel;

			var stackLayout = new StackLayout { };

			var sessionListView = new ListView (){ BackgroundColor = Color.White, };

			var sessionItemTemplate = new DataTemplate (typeof(TextCell));

      sessionItemTemplate.SetBinding(TextCell.TextProperty, "Title");
      sessionItemTemplate.SetValue(TextCell.TextColorProperty, Color.FromHex("ae3814"));
      sessionItemTemplate.SetBinding(TextCell.DetailProperty, "Description");
      sessionItemTemplate.SetValue(TextCell.DetailColorProperty, Color.Black);
      //sessionItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "ImageUrl");


			sessionListView.ItemTemplate = sessionItemTemplate;
			sessionListView.BindingContext = _viewModel;
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

	  protected override void OnAppearing()
	  {
      _viewModel.LoadSessionsAsync(_day);

	    base.OnAppearing();
	  }
	}
}

