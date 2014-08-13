using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Services;

namespace Core.ViewModels
{
	public class SessionViewModel : BaseViewModel
	{
		public SessionViewModel (ThatConfService thatConfService)
		{
			_thatConfService = thatConfService;
		}

		readonly ThatConfService _thatConfService;
		int _day;
		const string _thatConfBaseUrl = "https://www.thatconference.com";
		readonly List<SessionItemTemplate> _sessionTemplates = new List<SessionItemTemplate> ();

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

		private ObservableCollection<SessionItemTemplate> _sessionList;

		private Dictionary<DateTime, Dictionary<int, List<SessionItemTemplate>>> _sessionsByDay = new Dictionary<DateTime, Dictionary<int, List<SessionItemTemplate>>> ();

		public ObservableCollection<SessionItemTemplate> SessionList {
			get {
				if (_sessionList == null) {
					_sessionList = new ObservableCollection<SessionItemTemplate> ();
				}
				return _sessionList;
			}
			set { ChangeAndNotify (ref _sessionList, value); }
		}


		public async Task LoadSessionsAsync (int day)
		{
			_day = day;
			var sessions = await _thatConfService.GetSessionsAsync ();

			foreach (var session in sessions) {
				var description = "That Conference";
				var imageUrl = "ThatConference.png";

				var speaker = session.Speakers.FirstOrDefault ();

				if (speaker != null) {
					description = speaker.FirstName + " " + speaker.LastName + " " + session.ScheduledDateTime.ToString ("t") + " [Room : " + session.ScheduledRoom + "]";
					imageUrl = string.Format ("{0}{1}?{2}", _thatConfBaseUrl, speaker.HeadShot, "w=50&h=50&mode=crop");
				}

				var sessionDate = session.ScheduledDateTime.Date;
				var sessionTime = session.ScheduledDateTime.TimeOfDay.Hours;

				if (!_sessionsByDay.ContainsKey (sessionDate)) {
					var sessionsByTimeSpan = new Dictionary<int, List<SessionItemTemplate>> ();
					sessionsByTimeSpan.Add (sessionTime, new List<SessionItemTemplate> { new SessionItemTemplate {
							Title = session.Title,
							Description = description,
							ImageUrl = imageUrl,
							SessionDate = sessionDate,
							SessionTime = session.ScheduledDateTime.TimeOfDay
						}
					});

					_sessionsByDay.Add (sessionDate, sessionsByTimeSpan);
				} else {
					if (!_sessionsByDay [sessionDate].ContainsKey (sessionTime)) {
						_sessionsByDay [sessionDate].Add (sessionTime, new List<SessionItemTemplate> {  new SessionItemTemplate {
								Title = session.Title,
								Description = description,
								ImageUrl = imageUrl,
								SessionDate = sessionDate,
								SessionTime = session.ScheduledDateTime.TimeOfDay
							}
						}
						);
					} else {
						_sessionsByDay [sessionDate] [sessionTime].Add (new SessionItemTemplate {
							Title = session.Title,
							Description = description,
							ImageUrl = imageUrl,
							SessionDate = sessionDate,
							SessionTime = session.ScheduledDateTime.TimeOfDay
						});
					}
				}
			}

			FilterSessionsByDayAndHour (day, (int)Hour);
		}

		private void FilterSessionsByDayAndHour (int day, int hour)
		{
			if (_sessionsByDay.Any () && hour != 0) {
				var dayIndex = day - 1;

				var startDate = _sessionsByDay.Keys.ToArray () [dayIndex];

				var filteredSessions = new List<SessionItemTemplate> ();

				if (_sessionsByDay [startDate].ContainsKey (hour)) {
					filteredSessions = _sessionsByDay [startDate] [hour].ToList ();
				}

				SessionList.Clear ();

				foreach (var session in filteredSessions) {
					SessionList.Add (session);
				}
			}
		}
	}
}

