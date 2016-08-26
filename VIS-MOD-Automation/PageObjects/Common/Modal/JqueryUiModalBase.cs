using System;
using System.Linq;
using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Modal
{
	public abstract class JqueryUiModal : IModal
	{
		protected IWebElement ModalContainer
		{
			get { return Browser.Current.FindElements(By.ClassName("ui-dialog")).SingleOrDefault(x => x.IsDisplayed()); } 
		}

		protected IWebElement BodyContainer
		{
			get { return ModalContainer.FindElement(By.ClassName("ui-dialog-content")); }
		}

		protected IWebElement ButtonContainer
		{
			get { return ModalContainer.FindElement(By.ClassName("ui-dialog-buttonset")); }
		}

		public bool IsDisplayed
		{
			get
			{
				return ModalContainer != null
				       && ModalContainer.Displayed
                       && ModalContainer.FindElement(By.ClassName("ui-dialog-title")).Text.Trim() == Title;
			}
		}

		public virtual string Title
		{
			get { return ModalContainer.FindElement(By.Id("ui-dialog-title-1")).Text; }
		}

		public virtual void Ok()
		{
			GetButton("Ok").ClickAndWait(0.5);
		}

		public virtual TPage Ok<TPage>(string expectedPageTitle = null, double timeoutSec = 10) where TPage : IPage, new()
		{
			var page = GetButton("Ok").ClickAndWaitForPage<TPage>(expectedPageTitle, timeoutSec);
			Wait.For(1);
			return page;
		}

		public void Close()
		{
			ModalContainer.FindElement(By.ClassName("ui-dialog-titlebar-close")).ClickAndWait(0.5);
		}

		protected IWebElement GetButton(string buttonLabel)
		{
			return ButtonContainer
				.FindElements(By.ClassName("ui-button-text"))
				.Single(x => x.Text.Equals(buttonLabel, StringComparison.InvariantCultureIgnoreCase));
		}

        public virtual TPage Cancel<TPage>(string expectedPageTitle = null, double timeoutSec = 10) where TPage : IPage, new()
	    {
	        var page = ModalContainer.FindElements(By.TagName("button"))
	            .First(b => b.FindElement(By.TagName("span")).Text.Trim().Equals("Cancel")
	                        && b.Displayed && b.Enabled).ClickAndWaitForPage<TPage>(expectedPageTitle, timeoutSec);
            Wait.For(1);
            return page;
	    }
	}
}