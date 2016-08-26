using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace SeleniumUtils
{
	public static class Browser
	{
	    public static IWebDriver LaunchPhantom()
	    {
	        Console.Out.WriteLine("Configuring PhantomJS dependency...");
            var test = new WebClient(); try
            {
                test.DownloadString("http://localhost:2880");
            }
            catch (WebException ex)
            {
                if (ex.Response != null && ex.Response.ContentType == "text/plain")
                {
                    return new RemoteWebDriver(new Uri("http://localhost:2880"),
                        new DesiredCapabilities("phantomjs", "phantomjs-1.8.1+ghostdriver-1.0.2",
                            Platform.CurrentPlatform));
                }
            }
	        Console.Out.WriteLine("BrucePhantomJS is not running, so attempting to launch...");
            var phantomJSDriverService = PhantomJSDriverService.CreateDefaultService("../../../Selenium/");
            phantomJSDriverService.ProxyType = "none";
	        phantomJSDriverService.Port = 2880;
            return new PhantomJSDriver(phantomJSDriverService);
	    }

		static Browser()
		{
			//Current = new InternetExplorerDriver("../../../Selenium/");
            /*var firefoxProfile = new FirefoxProfile();
            Current = new FirefoxDriver(firefoxProfile);
            if (File.Exists("C:\\UnitTests\\use-phantom.txt"))
            {
                Current = LaunchPhantom();
                return;
            } */
            Console.WriteLine("The browser was started");
            //Current = new ChromeDriver("C:\\Ravi\\Automation\\SpecflowSelenium\\Selenium/");
            Current = new ChromeDriver("../../../Selenium/");
            //Current = new PhantomJSDriver("../../../Selenium/");
        }

		public static IWebDriver Current { get; set; }

		public static IJavaScriptExecutor JsExecuter
		{
			get { return (IJavaScriptExecutor) Current; }
		}

		public static string Title
		{
			get { return Current.Title; }
		}

		public static Screenshot GetScreenshot()
		{
			return ((ITakesScreenshot) Current).GetScreenshot();
		}

		public static void ReleaseMemory()
		{
			Current.DismissAllAlerts();

			var originalUrl = Current.Url;

			JsExecuter.ExecuteScript("window.open('http://www.google.com', '_blank', 'width=1280,height=2000')");
			Current.Close();

			try
			{
				Current.SwitchTo().Window(Current.WindowHandles.Last());
				Current.Navigate().GoToUrl(originalUrl);
			}
			catch (InvalidOperationException)
			{
				GetScreenshot().SaveAsFile(String.Format("ReleaseMemory_{0}.jpg", DateTime.UtcNow.ToFileTime()), ImageFormat.Jpeg);
				Thread.CurrentThread.Abort();
			}
		}
	}
}