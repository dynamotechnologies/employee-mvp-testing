using PageObjects.Common.Page;
using TechTalk.SpecFlow;

namespace StepDefinitions.UI
{
    [Binding]
    public class ButtonSteps : PageBase
    {
        [When(@"I click on the button using element id ""(.*)""")]
        public void ClickOnTheButtonUsingElementId(string elementId)
        {
            ButtonPageObject.ClickButtonUsingElementId(elementId);
        }

        [When(@"I click on the button using name attribute ""(.*)""")]
        public void ClickOnTheButtonUsingNameAttribute(string nameAttribute)
        {
            ButtonPageObject.ClickButtonUsingNameAttribute(nameAttribute);
        }

        [When(@"I click on the button using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void ClickOnTheButtonUsingTagAttribute(string tag, string attrType, string attrValue)
        {
            ButtonPageObject.ClickButtonUsingTagAttribute(tag, attrType, attrValue);
        }

        [When(@"I click on the button using class name ""(.*)""")]
        public void ClickOnTheButtonUsingClassName(string className)
        {
            ButtonPageObject.ClickButtonUsingClassName(className);
        }

        [When(@"I click on the button using cssselector ""(.*)""")]
        public void ClickOnTheButtonUsingCssSelector(string names)
        {
            ButtonPageObject.ClickButtonUsingCssSelector(names);
        }

        [When(@"I click on the submit button labeled ""(.*)""")]
        public void ClickOnTheSubmitButtonLabeled(string label)
        {
            ButtonPageObject.ClickOnSubmitButtonLabeled(label);
        }

        [When(@"I click on the button using label ""(.*)""")]
        public void ClickOnButtonUsingLabel(string label)
        {
            ButtonPageObject.ClickOnButtonUsingLabel(label);
        }

        [When(@"I click on the submit button containing the text ""(.*)""")]
        public void ClickOnTheSubmitButtonContainingTheText(string buttonText)
        {
            ButtonPageObject.ClickOnSubmitButtonContainingText(buttonText);
        }

    }
}
