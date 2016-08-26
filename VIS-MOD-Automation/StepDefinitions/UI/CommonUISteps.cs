using System.Configuration;
using NUnit.Framework;
using PageObjects.Common.Page;
using SeleniumUtils;
using TechTalk.SpecFlow;
using static System.Int32;

namespace StepDefinitions.UI
{
    [Binding]

    public class CommonUiSteps : Steps
    {
        [Given(@"I navigate to the url ""(.*)"" titled ""(.*)""")]
        public void NavigateToThePage(string url, string pageTitle)
        {
            TestingBase.SetupBrowser();
            PageBase.NavigateToUrl(url, pageTitle);
        }

        [Given(@"I navigate to the url for ""(.*)"" titled ""(.*)""")]
        public void NavigateToASite(string siteName, string pageTitle)
        {
            var url = ConfigurationManager.AppSettings[siteName];
            TestingBase.SetupBrowser();
            PageBase.NavigateToUrl(url, pageTitle);
        }

        [Given(@"the page title is ""(.*)""")]
        [When(@"the page title is ""(.*)""")]
        [Then(@"the page title is ""(.*)""")]
        public void ThePageTitleIs(string expectedTitle)
        {
            Assert.True(PageBase.PageTitle(expectedTitle));
        }

        [Given(@"the page is loaded")]
        public void GivenThePageIsLoaded()
        {
            // do nothing; after checking a new page title at the start of a scenario using - Then the page title is "xxx", avoid 
            // checking the same title again at the start of the next scenario using - Given the page title is "xxx"
        }

        [Then(@"the page contains the text ""(.*)""")]
        public void PageContainsTheText(string text)
        {
            Assert.True(PageBase.PageContainsText(text));
        }

        [Then(@"the page contains the text (.*) from the following table")]
        public void PageContainsTheTextFromTheFollowingTable(string text)
        {
            Assert.True(PageBase.PageContainsText(text));
        }

        [Then(@"the page ""(.*)"" elements contain the text (.*) from the following table")]
        public void PageElementsContainTheTextFromTheFollowingTable(string elementType, string text)
        {
            /*
                This method checks for all elements of the specified elementType within the page that contain the required text.
                Note: "Then the page contains the text "xxx" - this checks for the text contained directly within the <body> 
                      tag, not within elements inside the body tag.
            */
            Assert.True(PageBase.PageElementsContainText(elementType, text));
        }

        [When(@"I search the browser for the text ""(.*)"" using element id ""(.*)""")]
        public void SearchTheBrowserForTheText(string searchText, string elementId)
        {
            TextBoxPageObject.SearchBrowserUsingElementId(searchText, elementId);
        }


        [When(@"I click on the link containing the text ""(.*)""")]
        [Then(@"I click on the link containing the text ""(.*)""")]
        public void ClickOnTheLinkText(string linkText)
        {
            PageBase.ClickOnLinkText(linkText);
        }

        [When(@"I click on the alert generating link containing the text ""(.*)""")]
        [Then(@"I click on the alert generating link containing the text ""(.*)""")]
        public void ClickOnAlertGeneratingLinkText(string linkText)
        {
            PageBase.ClickOnAlertGeneratingLinkText(linkText);
        }

        [When(@"I wait until ""(.*)"" seconds and click on the link containing the text ""(.*)""")]
        public void WaitUntilSecondsAndClickOnTheLinkText(string waitTime, string linkText)
        {
            var waitUntilSeconds = Parse(waitTime);
            PageBase.ClickOnLinkText(linkText, waitUntilSeconds);
        }

        [When(@"I click on the link using element id ""(.*)""")]
        public void WhenIClickOnTheLinkUsingElementId(string elementId)
        {
            PageBase.ClickOnLinkUsingElementId(elementId);
        }

        [When(@"I click on the link using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void ClickOnTheLinkUsingTagAttributeTypeAttributeValue(string tag, string attrType, string attrValue)
        {
            PageBase.ClickOnLinkUsingTagAttribute(tag, attrType, attrValue);
        }

        [When(@"I close the current page")]
        [Then(@"I close the current page")]
        public void CloseTheCurrentPage()
        {
            // Close the current page; If this is the last page, quit chrome driver as well
            PageBase.CloseBrowser();
        }

        [When(@"I switch to the last browser")]
        public void SwitchToTheLastBrowser()
        {
            // When a new browser pops up after clicking on a link, it becomes the last browser
            // After closing this browser, previous browser becomes the last browser
            PageBase.SwitchToLastBrowser();
        }

        [When(@"I wait for ""(.*)"" second\(s\)""")]
        public void WaitForSeconds(int waitTime)
        {
            Wait.For(waitTime);
        }

        [When(@"I send the control-return keys to the element using class name ""(.*)""")]
        public void SendKeysToTheElementUsingClassName(string className)
        {
            PageBase.SendControlReturnKeysToElementUsingElementBy(className);
        }

        [When(@"I switch to iframe using element id ""(.*)""")]
        public void WhenISwitchToIframeUsingElementId(string elementId)
        {
            PageBase.SwitchToFrameUsingElementId(elementId);
        }

        [Then(@"I switch back to the body of the page")]
        public void ThenISwitchBackToTheBodyOfThePage()
        {
            PageBase.SwitchFromFrameToPage();
        }

        /*/=============================
        [When(@"element exists using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void WhenElementExistsUsingTagAttributeTypeAttributeValue(string tag, string p1, string attrType, string attrValue)
        {
            var elementFound = PageBase.ElementExistsUsingTagAttribute(tag, attrType, attrValue);
            if (!elementFound) ScenarioContext.Current.Pending();
        }

        [When(@"I skip scenario if condition is false")]
        public void WhenISkipScenarioIfConditionIsTrue(Table table)
        {
            var step = table.Rows[0]["condition"];
            var v1 = "input";
            var v2 = "value";
            var v3 = "Confirm Company Validity";
            Given("I navigate to \"www.gmail.com\"");
            //When(step);
            When($"element exists using tag {v1} attribute type {v2} attribute value {v3}");
        }

        *///=============================

        [When(@"I skip scenario if element is missing using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void SkipScenarioIfElementIsMissingUsingTagAttributeTypeAttributeValue(string tag, string attrType,
            string attrValue)
        {
            var elementFound = PageBase.ElementExistsUsingTagAttribute(tag, attrType, attrValue);
            if (!elementFound) ScenarioContext.Current.Pending();
        }

        [When(@"I upload file ""(.*)"" using element id ""(.*)""")]
        public void WhenIUploadFileUsingElementId(string filePath, string elementId)
        {
            PageBase.UploadFileUsingElementId(filePath, elementId);
        }

        [When(@"I save the data using element id ""(.*)"" into the variable ""(.*)""")]
        public void SaveTheDataUsingElementIdIntoTheVariable(string elementId, string contextVar)
        {
            FeatureContext.Current[contextVar] = PageBase.GetTextUsingElementId(elementId);
        }

        [When(@"I accept the alert dialog")]
        public void AcceptTheAlertDialog()
        {
            Browser.Current.DismissAllAlerts();
        }
    }
}
