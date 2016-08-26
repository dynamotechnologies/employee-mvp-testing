using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumUtils
{
	public static class WebElementExtensions
	{
		#region Click and Wait

		public static IAlert ClickAndWaitForAlert(this IWebElement field, double timeoutSec = 10)
		{
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
            field.Click();
            Wait.UntilDocumentReady();
			IAlert alert = null;
			Wait.Until(browser => browser.TryGetAlert(out alert));
			return alert;
		}

		public static void ClickAndWaitForPage(this IWebElement field, string expectedPageTitle, double timeoutSec = 10)
		{
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
            field.Click();
            Wait.UntilDocumentReady();
			Wait.Until(browser => browser.Title.Contains(expectedPageTitle));
		}

//		public static TPage ClickAndWaitForPage<TPage>(this IWebElement field, string expectedPageTitle = null, double timeoutSec = 10)
//			where TPage : IPage, new()
        public static TPage ClickAndWaitForPage<TPage>(this IWebElement field, string expectedPageTitle, double timeoutSec = 10)
            where TPage : IPage, new()
        {
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
            field.Click();
            Wait.UntilDocumentReady();
            // Wait.Until(browser => browser.Title.Contains(expectedPageTitle ?? new TPage().PageTitle), timeoutSec);
            Wait.Until(browser => browser.Title.Contains(expectedPageTitle), timeoutSec);
            return new TPage();
		}

		public static void SubmitAndWaitForPage(this IWebElement field, string expectedPageTitle, double timeoutSec = 10)
		{
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.Submit();
            Wait.UntilDocumentReady();
			Wait.Until(browser => browser.Title.Contains(expectedPageTitle));
		}

        //		public static TPage SubmitAndWaitForPage<TPage>(this IWebElement field, string expectedPageTitle = null,
        //		                                                double timeoutSec = 10)
        //			where TPage : IPage, new()
        public static TPage SubmitAndWaitForPage<TPage>(this IWebElement field, string expectedPageTitle,
                                                                double timeoutSec = 10)
                    where TPage : IPage, new()
        {
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.Submit();
            Wait.UntilDocumentReady();
            //Wait.Until(browser => browser.Title.Contains( expectedPageTitle ?? new TPage().PageTitle));
            Wait.Until(browser => browser.Title.Contains(expectedPageTitle));
            return new TPage();
		}

		public static void ClickAndWaitForJqueryUiModal(this IWebElement field, string expectedModalTitle, double timeoutSec = 10)
		{
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.Click();
            Wait.UntilDocumentReady();
			Wait.Until(browser =>
				{
                    var modalContainer = browser.FindElements(By.ClassName("ui-dialog")).First(u => u.Enabled && u.Displayed);
					var isModalDisplayed = modalContainer != null && modalContainer.Displayed;
					return isModalDisplayed && modalContainer.FindElement(By.ClassName("ui-dialog-title")).Text.Trim() == expectedModalTitle;
				});
			
		}

		public static TModal ClickAndWaitForJqueryUiModal<TModal>(this IWebElement field, string expectedModalTitle = null,
		                                                  double timeoutSec = 10) where TModal : IModal, new()
		{
            expectedModalTitle = expectedModalTitle ?? new TModal().Title;
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.ClickAndWaitForJqueryUiModal(expectedModalTitle, timeoutSec);
            Wait.UntilDocumentReady();
			return new TModal();
		}

		public static void ClickAndWaitForJqModal(this IWebElement field, string expectedModalTitle, double timeoutSec = 10)
		{
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.Click();
            Wait.UntilDocumentReady();
			Wait.Until(browser =>
			{
				var modalContainer = browser.FindElementNoException(By.ClassName("jqmWindow"));
				var isModalDisplayed = modalContainer != null && modalContainer.Displayed;
				return isModalDisplayed && modalContainer.FindElement(By.ClassName("dialogHeader-title")).Text.Trim() == expectedModalTitle;
			});
		}

		public static TModal ClickAndWaitForJqModal<TModal>(this IWebElement field, string expectedModalTitle = null,
																											double timeoutSec = 10) where TModal : IModal, new()
		{
			expectedModalTitle = expectedModalTitle ?? new TModal().Title;
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
			field.ClickAndWaitForJqModal(expectedModalTitle, timeoutSec);
            Wait.UntilDocumentReady();
			return new TModal();
		}

		public static void ClickAndWait(this IWebElement field, double waitFor = 1)
        {
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.For(waitFor);
            Wait.UntilDocumentReady();
			field.Click();
            Wait.UntilDocumentReady();
			Wait.For(waitFor);
		}

		public static bool ClickAndWait(this string elementId, double waitFor = 1)
		{
			var element = Browser.Current.FindElementNoException(By.Id(elementId));
			if (element != null)
			{
                Wait.Until(b => element.Enabled);
                Wait.UntilDocumentReady();
				element.ClickAndWait(waitFor);
                Wait.UntilDocumentReady();
				return true;
			}
			return false;
		}

        public static void ClickAndWaitForDocumentReady(this IWebElement field)
        {
            Wait.Until(b => field.Displayed && field.Enabled);
            Browser.JsExecuter.ExecuteScript(string.Format("window.scrollTo(0, {0});", field.Location.Y));
            Wait.UntilDocumentReady();
            field.Click();
            Wait.UntilDocumentReady();
        }

		#endregion

		#region Find Element

		public static IWebElement FindElementById(this string id)
		{
			return Browser.Current.FindElementNoException(By.Id(id));
		}

		public static IWebElement FindElementByName(this string name)
		{
			return Browser.Current.FindElementNoException(By.Name(name));
		}

		public static IWebElement FindLinkByText(this string linkText, double waitTime)
		{
            return Browser.Current.FindElementNoException(By.LinkText(linkText), waitTime);
		}

		public static IWebElement FindElementByClass(this string name)
		{
            return Browser.Current.FindElementNoException(By.ClassName(name));
        }

        public static IWebElement FindElementByCssSelector(this string names)
        {
            return Browser.Current.FindElementNoException(By.CssSelector(names));
        }

        public static IWebElement FindElementByTag(this string tag)
		{
			return Browser.Current.FindElementNoException(By.TagName(tag));
		}

	    public static IWebElement FindElementByTagAttribute(this string tag, string attrType, string attrValue)
	    {
            var element = Browser.Current.FindElements(By.TagName(tag))
                .First(t => t.GetAttribute(attrType).Contains(attrValue) && t.Enabled && t.IsDisplayed());
            return element;
;	    }

	    public static IWebElement FindButtonByValue(this IWebElement field, string value)
		{
			return field.FindElementNoException(By.XPath(string.Format(".//input[(@type='button' or @type='submit') and @value='{0}']", value)));
		}

	    public static IWebElement FindElementContainingText(this IWebElement field, string text, string tagName = null)
	    {
	        tagName = tagName ?? "*";
	        return
	            field.FindElements(By.XPath(string.Format(".//{0}[contains(normalize-space(.),'{1}')]", tagName, text)))
	                .Single(e => e.Enabled && e.IsDisplayed());
	    }

	    public static IWebElement FindElementNoException(this IWebElement field, By by)
        {
            try
            {
                return field.FindElements(by).Single(e => e.Enabled && e.IsDisplayed());

            }
            catch (NoSuchElementException)
            {
            }

            return null;
        }

		#endregion

		#region Knockout

		public static IEnumerable<IWebElement> FindElementsByKoAttr(this IWebElement field, By by, string attrName,
		                                                            string attrValue)
		{
			return field.FindElements(by).Where(x => x.GetKoBindingAttr(attrName) == attrValue).ToList();
		}

		public static IWebElement FindElementByKoAttr(this IWebElement field, By by, string attrName, string attrValue)
		{
			return field.FindElementsByKoAttr(by, attrName, attrValue).Single();
		}

		public static IDictionary<string, string> GetKoBindingAttrs(this IWebElement field)
		{
			var databindAttr = field.GetAttribute("data-bind");

			return !string.IsNullOrEmpty(databindAttr)
				       ? databindAttr.Trim().Split(',')
					         .Select(bindingAttr => bindingAttr.Split(':'))
					         .ToDictionary(k => k[0].Trim(), v => v[1].Trim())
				       : new Dictionary<string, string>();
		}

		public static string GetKoBindingAttr(this IWebElement field, string attrName)
		{
			var koBindingAttrs = field.GetKoBindingAttrs();
			return koBindingAttrs.ContainsKey(attrName) ? koBindingAttrs[attrName] : null;
		}

		#endregion

		#region Write data

		public static void SetData(this IWebElement field, string data)
		{
			/* Clear the content for 'input' tags only
			   field.Clear does not work for editable divs, hence this hack
               If element is not editable (or StaleElementReference), do nothing
            */
		    try
		    {
                if (field.TagName != "select" && field.GetAttribute("type") != "file")
                {
                    field.SendKeys(Keys.Control + "a");
                    for (var i = 0; i < 10; i++) field.SendKeys(Keys.Backspace); // ctrl-a does not highlight some fields
                }
                //write the new content
                field.SendKeys(data);
            }
		    catch (StaleElementReferenceException)
		    {
		    }
		}

		#endregion

		#region Actions

		public static void Mouseover(this IWebElement element)
		{
			new Actions(Browser.Current).MoveToElement(element).Perform();
			//Wait.For(1);
		}

		public static void ToggleChecked(this IWebElement element, bool flag)
		{
			if (flag && element.Selected) return;
			if (!flag && !element.Selected) return;
			element.Click();
		}

		#endregion

		#region Get Element Data

		public static string GetTextOfElement(this string elementId)
		{
			var element = FindElementById(elementId);
			return element == null ? null : element.Text;
		}

		public static string GetInnerText(this IWebElement element)
		{
			const string jsFindFirstTextNodeValue = "return arguments[0].firstChild.nodeValue";
			var text = Browser.JsExecuter.ExecuteScript(jsFindFirstTextNodeValue, element).ToString();
			return text;
		}

		#endregion

		#region Scroll

		public static void ScrollTo(this IWebElement element)
		{
			Browser.JsExecuter.ExecuteScript("arguments[0].scrollIntoView(true);", element);
			Wait.For(0.5);
		}
		#endregion

		#region Is Displayed

		public static bool IsDisplayed(this IWebElement field)
		{
			if (field == null) return false;

			var isDisplayed = false;
			try
			{
				isDisplayed = field.Displayed;
			}
			catch (InvalidOperationException exp)
			{
				if (exp.Message.Contains("Error determining if element is displayed (UnexpectedJavaScriptError)"))
					return field.IsDisplayed();
				throw;
			}

			return isDisplayed;
		}

		#endregion
	}
}