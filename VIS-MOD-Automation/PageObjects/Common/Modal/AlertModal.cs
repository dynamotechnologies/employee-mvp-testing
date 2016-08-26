using OpenQA.Selenium;

namespace PageObjects.Common.Modal
{
	public class AlertModal : JqueryUiModal
	{
		public override string Title
		{
			get { return "Alert"; }
		}

		public string AlertMessage
		{
			get { return ModalContainer.FindElement(By.ClassName("bruceAlertDialog")).Text; }
		}
	}
}