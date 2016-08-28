using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class PageBase : IPage
    {
        //public virtual bool PageTitle(string expectedTitle)
        public static bool PageTitle(string expectedTitle)
        {
            Wait.UntilDocumentReady();
            Wait.Until(browser => browser.Title.Contains(expectedTitle));
            return true;
        }

        public static PageBase NavigateToUrl(string url, string pageTitle)
        {
            return Browser.Current.GotoPage<PageBase>(url, pageTitle);
        }

        //public bool PageContainsText(string text)
        public static bool PageContainsText(string text)
        {
            return "body".FindElementByTag().Text.Contains(text);
        }

        public static void ClickOnLinkText(string linkText, double waitTime = 1)
        {
            linkText.FindLinkByText(waitTime).ClickAndWait();
        }

        public static void ClickOnAlertGeneratingLinkText(string linkText, double waitTime = 1)
        {
            /* since an alert is expected, avoid ClickAndWait() since it uses Wait.UntilDocumentReady()
               which waits for the document to load while the alert is present!!
            */
            linkText.FindLinkByText(waitTime).Click();
        }

        public static void ClickOnLinkUsingElementId(string elementId)
        {
            elementId.FindElementById().ClickAndWait();
        }

        public static void ClickOnLinkUsingTagAttribute(string tag, string attrType, string attrValue)
        {
            tag.FindElementByTagAttribute(attrType, attrValue).ClickAndWait();
        }

        protected virtual IWebElement BodyContainer
        {
            get { return Browser.Current.FindElement(By.ClassName("content")); }
        }

        public static void SendControlReturnKeysToElementUsingElementBy(string className)
        {
            className.FindElementByClass().SendKeys(Keys.Control + Keys.Return);
        }
        
        public static void SwitchToLastBrowser()
        {
            Browser.Current = Browser.Current.SwitchTo().Window(Browser.Current.WindowHandles.Last());
        }

        public static void CloseBrowser()
        {
            Console.WriteLine("Closing Browser...");
            Browser.Current.Close();
            try
            {
                // If this is not the last browser, switch to previous browser
                if (Browser.Current.WindowHandles.Count >= 1)
                    SwitchToLastBrowser();
            }
            catch (InvalidOperationException)
            {
                // If this was the last browser, after closing browser, Browser.Current.WindowHandles() raises exception
                // Now quit driver as well
                Browser.Current.Quit();
            }
        }

        public static void SwitchToFrameUsingElementId(string elementId)
        {
            Browser.Current.SwitchTo().Frame(elementId.FindElementById());
        }

        public static void SwitchFromFrameToPage()
        {
            Browser.Current.SwitchTo().DefaultContent();
        }

        public static bool ElementExistsUsingTagAttribute(string tag, string attrType, string attrValue)
        {
            try
            {
                var element = tag.FindElementByTagAttribute(attrType, attrValue);
                return element.Enabled && element.IsDisplayed();
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is InvalidOperationException)
                    return false;
            }
            return false;
        }
        
        public static void UploadFileUsingElementId(string filePath, string elementId)
        {
            var fileName = Path.Combine(Environment.CurrentDirectory, filePath);
            elementId.FindElementById().SetData(fileName);
        }

        public static string GetTextUsingElementId(string elementId)
        {
            return elementId.FindElementById().Text.Trim();
        }

        public static bool PageElementsContainText(string elementType, string searchText)
        {
            var elements = Browser.Current.FindElements(By.TagName(elementType));
            return elements.Any(e => e.Text.Contains(searchText));
        }

        //        public virtual bool IsCurrentPage
        //        {
        //            get { return Browser.Title.Contains(PageTitle); }
        //        }

        public static bool ElementOfPageContainsText(string elementId, string text, string attrType)
        {
            //            return elementId.FindElementById().[tableRowNumber - 1].FindElements(By.TagName("td"))
            //                .Any(t => t.FindElement(By.XPath($"//*[contains(text(), '{value}')]")).Enabled);
            //return elementId.FindElementById().FindElement(By.XPath($".//*[contains(text(), '{text}')]")).Enabled;
            var element = elementId.FindElementById().FindElementContainingText(text, attrType);
            return element.Enabled && element.Displayed;
        }

        public static string GenerateRandomString(int stringLength)
        {
            var randomString = Guid.NewGuid().ToString("n").Substring(0, stringLength);
            return randomString;
        }
    }
}
