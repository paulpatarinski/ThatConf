using System;
using System.Threading.Tasks;
using Core.Services;
using Core.ViewModels;
using Xamarin.Forms;

namespace Core.Pages
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

			var sessionItemTemplate = new DataTemplate (typeof(ImageCell));

			sessionItemTemplate.SetBinding (ImageCell.TextProperty, "Title");
			sessionItemTemplate.SetValue (ImageCell.TextColorProperty, Color.FromHex ("ae3814"));
			sessionItemTemplate.SetBinding (ImageCell.DetailProperty, "Description");
			sessionItemTemplate.SetValue (ImageCell.DetailColorProperty, Color.Black);
			sessionItemTemplate.SetBinding (ImageCell.ImageSourceProperty, "ImageUrl");


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

		protected override void OnAppearing ()
		{
			try {
				_viewModel.LoadSessionsAsync (_day);

			} catch (Exception) {
	      
				throw;
			}

			base.OnAppearing ();
		}
	}
}

