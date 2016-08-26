using System.Linq;
using OpenQA.Selenium;

namespace SeleniumUtils
{
    public static class WebDriverExtensions
    {
        #region Alert

        public static bool TryGetAlert(this IWebDriver browser, out IAlert alert)
        {
            try
            {
                alert = browser.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                alert = null;
                return false;
            }
        }

        public static void DismissAllAlerts(this IWebDriver browser)
        {
            IAlert alert;
            Wait.For(); // pause for a moment for any alert to show up
            var isAlertPresent = browser.TryGetAlert(out alert);
            while (isAlertPresent)
            {
                alert.Accept();
                isAlertPresent = browser.TryGetAlert(out alert);
            }
        }

        #endregion

        #region FindElement

        public static IWebElement FindElementNoException(this IWebDriver browser, By by, double waitTime = 30)
        {
            try
            {
                //Wait.Until(b => b.FindElements(by).Single(e => e.Enabled && e.IsDisplayed()).Enabled);
                //return browser.FindElements(by).Single(e => e.Enabled && e.IsDisplayed());
                Wait.Until(b => b.FindElements(by).First(e => e.Enabled && e.IsDisplayed()).Enabled, waitTime);
                return browser.FindElements(by).First(e => e.Enabled && e.IsDisplayed());
            }
            catch (NoSuchElementException)
            {
            }

            return null;
        }

        public static IWebElement FindElementContainingText(this IWebDriver browser, string text, string tagName = null)
        {
            tagName = tagName ?? "*";
            return
                browser.FindElement(By.XPath(string.Format(".//{0}[contains(normalize-space(.),'{1}')]", tagName, text)));
        }

        #endregion

        #region Navigate

        //        public static TPage GotoPage<TPage>(this IWebDriver browser, string url, string expectedPageTitle = null,
        //            int timeoutSec = 20) where TPage : IPage, new()
        public static TPage GotoPage<TPage>(this IWebDriver browser, string url, string expectedPageTitle,
            int timeoutSec = 20) where TPage : IPage, new()
        {
            // RK expectedPageTitle = expectedPageTitle ?? new TPage().PageTitle;
            browser.Navigate().GoToUrl(url);
            Wait.Until(b => b.Title.Contains(expectedPageTitle), timeoutSec);
            Wait.UntilDocumentReady();
            return new TPage();
        }

        public static TPage RefreshPage<TPage>(this IWebDriver browser, string expectedPageTitle = null)
            where TPage : IPage, new()
        {
            // RK expectedPageTitle = expectedPageTitle ?? new TPage().PageTitle;
            browser.Navigate().Refresh();
            Wait.Until(b => b.Title.Contains(expectedPageTitle));
            Wait.UntilDocumentReady();
            return new TPage();
        }

        #endregion
    }
}