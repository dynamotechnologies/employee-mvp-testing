using OpenQA.Selenium.Support.UI;
using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class SelectDropDownPageObject : PageBase
    {
        public static void SelectDropDownTextUsingElementId(string textToSelect, string elementId)
        {
            new SelectElement(elementId.FindElementById()).SelectByText(textToSelect);
        }

        public static string SelectedOptionUsingElementId(string elementId)
        {
            return new SelectElement(elementId.FindElementById()).SelectedOption.Text;
        }
    }
}
