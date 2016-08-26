using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Modal
{
	public class JqModal : IModal
	{
	    private static IEnumerable<IWebElement> ModalElements
	    {
            get { return Browser.Current.FindElements(By.ClassName("jqmWindow")); }
	    }

		protected IWebElement ModalContainer
		{
            get { return ModalElements.Any(e => e.Displayed) ? ModalElements.Single(x => x.Displayed) : null; } 
		}

		protected IWebElement BodyContainer
		{
			get { return ModalContainer.FindElement(By.ClassName("dialogBody")); }
		}

		protected IWebElement ButtonContainer
		{
			get { return ModalContainer.FindElement(By.ClassName("dialogButtonPane")); }
		}

		public bool IsDisplayed
		{
			get
			{
				return ModalContainer != null
				       && ModalContainer.FindElement(By.ClassName("dialogHeader-title")).Text.Trim() == Title;
			}
		}

		public virtual string Title
		{
			get { return ModalContainer.FindElement(By.Id("dialogHeader-title")).Text; }
		}

		public virtual void Ok()
		{
			ButtonContainer.FindElement(By.ClassName("dialogOK")).ClickAndWait(0.5);
		}

		public virtual TPage Ok<TPage>(string expectedPageTitle = null, double timeoutSec = 10) where TPage : IPage, new()
		{
			var page = ButtonContainer.FindElement(By.ClassName("dialogOK")).ClickAndWaitForPage<TPage>(expectedPageTitle, timeoutSec);
			return page;
		}

		public void Close()
		{
			ModalContainer.FindElement(By.ClassName("dialogHeader-close"))
				.ClickAndWait(0.5);
		}
	}
}