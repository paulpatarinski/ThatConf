using System;
using Xamarin.Forms;
using ThatConfXamarin;

namespace Core
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			return new SessionPage ();
		}
	}
}

