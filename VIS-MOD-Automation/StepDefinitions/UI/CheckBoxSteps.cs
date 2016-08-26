using PageObjects.Common.Page;
using TechTalk.SpecFlow;

namespace StepDefinitions.UI
{
    [Binding]
    public class CheckBoxSteps : PageBase
    {
        [When(@"I set the checkbox with element id ""(.*)"" to ""(.*)""")]
        public void WhenISetTheCheckboxWithElementIdTo(string elementId, bool value)
        {
            CheckBoxPageObject.SetCheckbox(elementId, value);
        }

        [When(@"I click on the radio button using element id ""(.*)""")]
        public void WhenIClickOnTheRadioButtonUsingElementId(string elementId)
        {
            CheckBoxPageObject.ClickOnRadioButton(elementId);
        }
    }
}
