using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumUtils
{
	public static class Wait
	{
		#region For
		public static void For(double waitTimeInSeconds = 3)
		{
			var waitTimeInMilliSeconds = (int)(waitTimeInSeconds * 1000);
			Thread.Sleep(waitTimeInMilliSeconds);
		}

		public static void For(TimeSpan timeSpan)
		{
			if (timeSpan.Ticks == 0) timeSpan = TimeSpan.FromSeconds(3);
			Thread.Sleep(timeSpan);
		} 
		#endregion

		#region Until
		public static void Until(Func<IWebDriver, bool> condition, double timeoutSec = 30)
		{
			var wait = new WebDriverWait(Browser.Current, TimeSpan.FromSeconds(timeoutSec));

			//wait throws sporadic StaleElementReferenceException
			wait.Until(b =>
			{
			    try
			    {
			        return condition(b);
			    }
			    catch (StaleElementReferenceException)
			    {
			        return false;
			    }
			    catch (InvalidOperationException)
			    {
			        return false;
			    }
			});
		}

		public static bool TryUntil(Func<IWebDriver, bool> condition, int timeoutSec = 10)
		{
			try
			{
				Until(condition, timeoutSec);
				return true;
			}
			catch (TimeoutException)
			{
				return false;
			}
		}

		public static void UntilDocumentReady()
		{
            Browser.Current.DismissAllAlerts(); // Dismiss any alerts
		    try
		    {
                Until(b => ((IJavaScriptExecutor)b).ExecuteScript("return document.readyState").Equals("complete"));
            }
		    catch (Exception)
		    {
                // if Alert appears during javascript execution, dimiss alert and re-execute javascript
                Browser.Current.DismissAllAlerts();
                Until(b => ((IJavaScriptExecutor)b).ExecuteScript("return document.readyState").Equals("complete"));
            }
		} 
		#endregion
	}
}
