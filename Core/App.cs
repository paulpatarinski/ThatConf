﻿using System;
using Core.Pages;
using Xamarin.Forms;

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

