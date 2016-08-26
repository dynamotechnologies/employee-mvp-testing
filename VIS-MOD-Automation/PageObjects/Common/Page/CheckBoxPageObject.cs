using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class CheckBoxPageObject : PageBase
    {
        public static void SetCheckbox(string elementId, bool value)
        {
            elementId.FindElementById().ToggleChecked(value);
        }

        public static void ClickOnRadioButton(string elementId)
        {
            elementId.FindElementById().ClickAndWait();
        }
    }
}
