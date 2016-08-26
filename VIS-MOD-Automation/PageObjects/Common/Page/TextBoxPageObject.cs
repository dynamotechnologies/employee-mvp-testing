using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class TextBoxPageObject : PageBase
    {
        public static void SetTextboxUsingElementId(string elementId, string value)
        {
            elementId.FindElementById().SetData(value);
        }

        public static void SetTextBoxUsingNameAttribute(string nameAttribute, string value)
        {
            nameAttribute.FindElementByName().SetData(value);
        }

        public static void SetTextboxUsingTagAttribute(string tag, string attrType, string attrValue, string value)
        {
            tag.FindElementByTagAttribute(attrType, attrValue).SetData(value);
        }

        public static void SetTextBoxUsingClassName(string className, string value)
        {
            className.FindElementByClass().SetData(value);
        }

        public static bool TextBoxUsingTagAttributeContainsValue(string tag, string attrType, string value)
        {
            var element = tag.FindElementByTagAttribute(attrType, value);
            return element.IsDisplayed() & element.Enabled;
        }

        public static bool TextBoxUsingElementIdContainsData(string elementId, string data)
        {
            return elementId.FindElementById().GetAttribute("value").Equals(data);
        }

        public static void SearchBrowserUsingElementId(string searchText, string elementId)
        {
            var element = Browser.Current.FindElement(By.TagName("body"));  // any element
            element.SendKeys(Keys.Control + "f");  // bring up the search textbox
            elementId.FindElementById().SetData(searchText);
        }
    }
}
