using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;

namespace ThatConfXamarin
{
	public class ThatConfService
	{

		public ThatConfService (HttpClient restClient)
		{
			_restClient = restClient;
		}

		private readonly HttpClient _restClient;

		private const string GetSessionsByTimeslotUrl =
			"https://www.thatconference.com/api3/Session/GetAcceptedSessions";

		public async Task<List<Session>> GetSessions ()
		{
			var jsonString = await _restClient.GetStringAsync (GetSessionsByTimeslotUrl);
			var list = JsonConvert.DeserializeObject<Session[]> (jsonString);

			return new List<Session> (list);
		}
	}
}

 