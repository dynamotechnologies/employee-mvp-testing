using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common
{
	public class CheckboxWithLabelItem
	{
		private readonly IWebElement _chkElement;

		public string Value { get; private set; }
		public string Text { get; private set; }

		public bool Selected
		{
			get { return _chkElement.Selected; }
			set { _chkElement.ToggleChecked(value); }
		}

		public CheckboxWithLabelItem(IWebElement chkElement, string text)
		{
			_chkElement = chkElement;
			Value = _chkElement.GetAttribute("value");
			Text = text;
		}
	}
}