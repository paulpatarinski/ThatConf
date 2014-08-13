using System;
using System.Collections.Generic;
using Core;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;

namespace ThatConfXamarin
{
	public class SessionViewModel : BaseViewModel
	{
		public SessionViewModel (ThatConfService thatConfService)
		{
			_thatConfService = thatConfService;
		}

		ThatConfService _thatConfService;
		int _day;
		const string _thatConfBaseUrl = "https://www.thatconference.com";
		List<SesssionItemTemplate> _sessionTemplates = new List<SesssionItemTemplate> ();

		public async Task LoadSessionsAsync (int day)
		{
			_day = day;
			var sessions = await _thatConfService.GetSessionsAsync ();

			foreach (var session in sessions) {
				var title = "That Conference";
				var imageUrl = "";

				var speaker = session.Speakers.FirstOrDefault ();

				if (speaker != null) {
					title = speaker.FirstName + " " + speaker.LastName + " " + session.ScheduledDateTime.ToString ("t") + " [Room :" + session.ScheduledRoom + "]";
					imageUrl = string.Format ("{0}{1}", _thatConfBaseUrl, speaker.HeadShot);
				}

				_sessionTemplates.Add (new SesssionItemTemplate () {
					Title = title, 
					Description = session.Title,
					ImageUrl = imageUrl,
					SessionDate = session.ScheduledDateTime.Date,
					SessionTime = session.ScheduledDateTime.TimeOfDay
				});
			}

			FilterSessionsByDayAndHour (day, (int)Hour);
		}

		private void FilterSessionsByDay (int day)
		{
			if (_sessionTemplates.Any ()) {
			
				var confDates = _sessionTemplates.GroupBy (session => session.SessionDate).Select (grp => grp.First ()).Select (x => x.SessionDate).ToList ();
				var dayIndex = day - 1;

				var startDate = confDates [dayIndex];

				var filteredSessions = _sessionTemplates.Where (x => x.SessionDate == startDate);

				SessionList.Clear ();

				foreach (var session in filteredSessions) {
					SessionList.Add (session);
				}
			}
		}

		private void FilterSessionsByDayAndHour (int day, int hour)
		{
			if (_sessionTemplates.Any ()) {

				FilterSessionsByDay (day);
				var filteredSessions = SessionList.Where (x => x.SessionTime.Hours == hour).ToList ();

				SessionList.Clear ();

				foreach (var session in filteredSessions) {
					SessionList.Add (session);
				}
			}
		}

		private double _hour;

		public double Hour {
			get {
				return _hour;
			}
			set {
				var rounderDouble = Math.Round (value);

				ChangeAndNotify (ref _hour, rounderDouble); 

				FilterSessionsByDayAndHour (_day, (int)_hour);
			}
		}

		private ObservableCollection<SesssionItemTemplate> _sessionList;

		public ObservableCollection<SesssionItemTemplate> SessionList {
			get {
				if (_sessionList == null) {
					_sessionList = new ObservableCollection<SesssionItemTemplate> ();
				}
				return _sessionList;
			}
			set { ChangeAndNotify (ref _sessionList, value); }
		}
	}
}

