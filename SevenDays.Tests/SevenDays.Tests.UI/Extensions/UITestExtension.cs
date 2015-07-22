using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace SevenDays.Tests.UI
{
	public static class UITestExtension
	{
		public static void WaitThenEnterText(this IApp app, Func<AppQuery, AppQuery> lambda, string text)
		{
			app.WaitForElement(lambda);
			app.EnterText(lambda, text);
		}
	}
}

