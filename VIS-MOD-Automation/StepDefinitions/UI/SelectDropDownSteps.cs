using NUnit.Framework;
using PageObjects.Common.Page;
using TechTalk.SpecFlow;

namespace StepDefinitions.UI
{
    [Binding]
    public class SelectDropDownSteps : PageBase
    {
        [When(@"I select the drop down text ""(.*)"" using element id ""(.*)""")]
        public void WhenISelectTheDropDownTextUsingElementId(string dropdownValue, string elementId)
        {
            SelectDropDownPageObject.SelectDropDownTextUsingElementId(dropdownValue, elementId);
        }

        [Then(@"the selected drop down text using element id ""(.*)"" is ""(.*)""")]
        public void ThenTheSelectedDropDownTextUsingElementIdIs(string elementId, string selectedText)
        {
            Assert.True(SelectDropDownPageObject.SelectedOptionUsingElementId(elementId).Equals(selectedText));
        }
    }
}
