using System;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;

namespace SeleniumUtils
{
	public class TestingBase
	{
        public static void SetupBrowser()
        {
            // =============
            // START BROWSER
            // =============
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    StartBrowser();
                    return;
                }
                catch (Exception)
                {
                    // Browser may be stuck; Retry
                }
            }
        }

	    private static void StartBrowser()
	    {
            CleanupChrome();
            Console.WriteLine("The browser was started");
            Browser.Current = new ChromeDriver("../../../Selenium/");
            CleanupChrome();
            var options = new ChromeOptions();
            options.AddArguments("test-type", "--disable-popup-blocking");
            options.AddExtensions("../../../Selenium/New-Tab-New-Window_v3.2.crx", "../../../Selenium/PDF-Viewer_v1.5.294.crx");
            Browser.Current = new ChromeDriver("../../../Selenium/", options);
            Browser.Current.Manage().Cookies.DeleteAllCookies();
            Browser.Current.Manage().Window.Maximize();
            Browser.Current.SwitchTo();
            Browser.JsExecuter.ExecuteScript(string.Format("window.focus();"));
            Browser.Current.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));	        
	    }

        private static void CleanupChrome()
        {
            foreach (var process in Process.GetProcessesByName("chrome"))
            {
                process.Kill();
            }
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
        }

        public void SignOut()
        {
            Console.WriteLine("The browser was quit");
            Browser.Current.Quit();
        }
	}
}