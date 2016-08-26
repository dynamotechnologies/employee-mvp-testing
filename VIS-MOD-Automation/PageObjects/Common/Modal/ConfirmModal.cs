using OpenQA.Selenium;

namespace PageObjects.Common.Modal
{
	public class ConfirmModal : JqueryUiModal
	{
		public override string Title
		{
			get { return "Confirm"; }
		}

		public string ConfirmMessage
		{
			get { return ModalContainer.FindElement(By.ClassName("bruceConfirmDialog")).Text; }
		}
	}
}