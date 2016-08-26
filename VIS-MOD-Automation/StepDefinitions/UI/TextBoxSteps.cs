using System;
using NUnit.Framework;
using PageObjects.Common.Page;
using TechTalk.SpecFlow;
using System.Configuration;

namespace StepDefinitions.UI
{
    [Binding]
    public class TextBoxSteps : PageBase
    {
        [When(@"I set the text box using element id ""(.*)"" with the value ""(.*)""")]
        public void SetTheTextBoxUsingElementIdWithTheValue(string elementId, string value)
        {
            TextBoxPageObject.SetTextboxUsingElementId(elementId, value);
        }

        [When(@"I set the text box using element id ""(.*)"" with the value for ""(.*)""")]
        public void SetTheTextBoxUsingElementIdWithKey(string elementId, string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            TextBoxPageObject.SetTextboxUsingElementId(elementId, value);
        }

        [When(@"I set the text box using name attribute ""(.*)"" with the value ""(.*)""")]
        public void SetTheTextBoxUsingNameAttributeWithTheValue(string nameAttribute, string value)
        {
            TextBoxPageObject.SetTextBoxUsingNameAttribute(nameAttribute, value);
        }

        [When(@"I set the text box using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)"" with the value ""(.*)""")]
        public void SetTheTextBoxUsingTagAttributeTypeAttributeValueWithTheValue(string tag, string attrType,
            string attrValue, string value)
        {
            TextBoxPageObject.SetTextboxUsingTagAttribute(tag, attrType, attrValue, value);
        }

        [When(@"I set the text box using class name ""(.*)"" with the value ""(.*)""")]
        public void SetTheTextBoxUsingClassNameWithTheValue(string className, string value)
        {
            TextBoxPageObject.SetTextBoxUsingClassName(className, value);
        }

        [Then(@"the text box using tag ""(.*)"" attribute type ""(.*)"" contains the value ""(.*)""")]
        public void TextBoxUsingTagAttributeContainsTheValue(string tag, string attrType, string value)
        {
            Assert.True(TextBoxPageObject.TextBoxUsingTagAttributeContainsValue(tag, attrType, value));
        }

        [When(@"I set the text box using element id ""(.*)"" with the value stored in the variable ""(.*)""")]
        public void SetTheTextBoxUsingElementIdWithTheValueStoredInTheVariable(string elementId, string contextVar)
        {
            TextBoxPageObject.SetTextboxUsingElementId(elementId, Convert.ToString(FeatureContext.Current[contextVar]));
        }

        [Then(@"the text box using element id ""(.*)"" contains the value ""(.*)""")]
        public void ThenTheTextBoxUsingElementIdContainsTheValue(string elementId, string value)
        {
            Assert.True(TextBoxPageObject.TextBoxUsingElementIdContainsData(elementId, value));
        }
    }
}
