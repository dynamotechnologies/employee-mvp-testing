using System.Linq;
using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class ButtonPageObject : PageBase
    {
        public static void ClickButtonUsingElementId(string elementId)
        {
            elementId.FindElementById().ClickAndWait();
        }

        public static void ClickButtonUsingNameAttribute(string nameAttribute)
        {
            nameAttribute.FindElementByName().ClickAndWait();
        }

        public static void ClickButtonUsingClassName(string className)
        {
            className.FindElementByClass().ClickAndWait();
        }

        public static void ClickButtonUsingCssSelector(string names)
        {
            names.FindElementByCssSelector().ClickAndWait();
        }

        public static void ClickButtonUsingTagAttribute(string tag, string attrType, string attrValue)
        {
            tag.FindElementByTagAttribute(attrType, attrValue).ClickAndWait();
        }

        public static void ClickOnSubmitButtonLabeled(string label)
        {
            /* Assumption: Submit buttons usually have tag="input", type="Submit", value=<input label param> */
            Browser.Current.FindElements(By.TagName("input"))
                .Single(t => t.GetAttribute("type").Equals("submit") && t.GetAttribute("value").Equals(label))
                .ClickAndWait();
        }

        public static void ClickOnButtonUsingLabel(string label)
        {
            /* Assumption: Some buttons have tag="input", type="button", value=<input label param> */
            Browser.Current.FindElements(By.TagName("input"))
                .Single(t => t.GetAttribute("type").Equals("button") && t.GetAttribute("value").Equals(label))
                .ClickAndWait();
        }

        public static void ClickOnSubmitButtonContainingText(string buttonText)
        {
            /* Assumption: All submit buttons containing text have tag="button", type="Submit", text=<input buttonText param> */
            Browser.Current.FindElements(By.TagName("button"))
                .Single(t => t.GetAttribute("type").Equals("submit") && t.Text.Equals(buttonText))
                .ClickAndWait();
        }
    }
}
