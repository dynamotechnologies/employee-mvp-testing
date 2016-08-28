using System;
using NUnit.Framework;
using PageObjects;
using PageObjects.Common.Page;
using TechTalk.SpecFlow;

namespace StepDefinitions.UI
{
    [Binding]
    public class TableSteps : PageBase
    {
        [When(@"I access the table using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void AccessTheTableUsingTagAttributeTypeAttributeValue(string tag, string attrType, string attrValue)
        {
            GlobalVars.Tag = tag;
            GlobalVars.AttributeType = attrType;
            GlobalVars.AttributeValue = attrValue;
            GlobalVars.TableNumber = -1; /* <table> tag has a single <tbody> tag (a single table) */
        }

        [When(@"I access table number ""(.*)"" using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)""")]
        public void AccessTableNumberUsingTagAttributeTypeAttributeValue(int tableNumber, string tag, string attrType, string attrValue)
        {
            GlobalVars.Tag = tag;
            GlobalVars.AttributeType = attrType;
            GlobalVars.AttributeValue = attrValue;
            GlobalVars.TableNumber = tableNumber; /* <table> tag has <tableNumber> <tbody> tags (multiple tables) */
        }

        [When(@"I access the table using element id ""(.*)""")]
        public void AccessTheTableUsingElementId(string elementId)
        {
            GlobalVars.Tag = "table";
            GlobalVars.AttributeType = "id";
            GlobalVars.AttributeValue = elementId;
            GlobalVars.TableNumber = -1;
        }

        [Then(@"the count of rows in the table is ""(.*)""")]
        public void CountOfRowsInTheTableIs(int count)
        {
            Assert.True(TablePageObject.CountOfTableRows.Equals(count));
        }

        [Then(@"the count of rows in the table is greater than ""(.*)""")]
        public void ThenTheCountOfRowsInTheTableIsGreaterThan(int minRowCount)
        {
            Assert.True(TablePageObject.CountOfTableRows > minRowCount);
        }

        [Then(@"table row number ""(.*)"" contains the value ""(.*)""")]
        public void TableRowNumberContainsTheValue(int tableRowNumber, string value)
        {
            Assert.True(TablePageObject.TableRowNumberContainsValue(tableRowNumber, value));
        }

        [Then(@"table row number ""(.*)"" column number ""(.*)"" contains the value ""(.*)""")]
        public void TableRowNumberColumnNumberContainsTheValue(int tableRowNumber, int columnNumber, string value)
        {
            Assert.True(TablePageObject.TableRowNumberColumnNumberContainsValue(tableRowNumber, columnNumber, value));
        }

        [Then(@"table row number (.*) contains the values (.*) from the following table")]
        public void TableRowNumberContainsTheValuesFromTheFollowingTable(int tableRowNumber, string value)
        {
            Assert.True((TablePageObject.TableRowNumberContainsValue(tableRowNumber, value)));
        }

        [When(@"I click on table row number ""(.*)"" in column number ""(.*)""")]
        public void ClickOnRowNumberInColumnNumber(int rowNumber, int columnNumber)
        {
            TablePageObject.ClickColumnNumberInRowNumber(columnNumber, rowNumber);
        }

        [When(@"I click on the button labeled ""(.*)"" in row number ""(.*)""")]
        public void ClickOnTheLabeledButtonInRowNumber(string label, int rowNumber)
        {
            TablePageObject.ClickOnLabeledButtonInRowNumber(label, rowNumber);
        }

        [When(@"I click on the button labeled ""(.*)"" in the table row number saved in the variable ""(.*)""")]
        public void ClickOnTheLabeledButtonInTableRowNumberSavedInVariable(string label, string contextVar)
        {
            /* Context stores actual index starting with 0; User-specified index starts with 1 */
            var rowNumber = Convert.ToInt32(FeatureContext.Current[contextVar]);
            TablePageObject.ClickOnLabeledButtonInRowNumber(label, rowNumber + 1);
        }

        [When(@"I click on the element using tag ""(.*)"" attribute type ""(.*)"" attribute value ""(.*)"" in row number ""(.*)""")]
        public void WhenIClickOnTheElementUsingTagAttributeTypeAttributeValueInRowNumber(string tag, string attrType, string attrValue, int rowNumber)
        {
            TablePageObject.ClickElementUsingTagAttributeInRowNumber(tag, attrType, attrValue, rowNumber);
        }

        [When(@"I save the table row number containing the data ""(.*)"" in column number ""(.*)"" into the variable ""(.*)""")]
        public void SaveIntoVariableTheTableRowNumberContainingTheDataInColumnNumber(string data, int columnNumber, string contextVar)
        {
            FeatureContext.Current[contextVar] = TablePageObject.GetTableRowNumberContainingTheDataInColumnNumber(data, columnNumber);
            /* In WebHQ, if new company status goes to ACTIVE following registration, PENDING status cannot be found. So work with ACTIVE company */
            if (FeatureContext.Current[contextVar].Equals(-1))
            FeatureContext.Current[contextVar] = 1;
        }

        [When(@"I save the data from table row number ""(.*)"" column number ""(.*)"" into the variable ""(.*)""")]
        public void SaveTheDataFromTableRowNumberColumnNumberIntoTheVariable(int rowNumber, int columnNumber, string contextVar)
        {
            FeatureContext.Current[contextVar] = TablePageObject.GetDataFromTableRowNumberColumnNumber(rowNumber, columnNumber);
        }

        [When(@"I skip scenario if table does not contain the data ""(.*)""")]
        public void SkipScenarioIfTableDoesNotContainTheData(string data)
        {
            var found = TablePageObject.TableContainsData(data);
            if (!found) ScenarioContext.Current.Pending();
        }
    }
}
