using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumUtils;

namespace PageObjects.Common.Page
{
    public class TablePageObject : PageBase
    {
        public static List<IWebElement> GetTableRowsByTagAttribute
        {
            /* 
            Before accessing any table, one of the following methods MUST be called using the following Step Definition:
            (a) I access the table using tag "<tag>" attribute type "<attribute type" attribute value "<attribute value>" 
            (b) I access table number "<tablenumber> using tag "<tag>" attribute type "<attribute type" attribute value "<attribute value>" 
            */
            get
            {
                var tableTag = Browser.Current.FindElements(By.TagName(GlobalVars.Tag))
                    .Single(
                        t =>
                            t.Displayed &&
                            t.GetAttribute(GlobalVars.AttributeType).Contains(GlobalVars.AttributeValue));

                if (GlobalVars.TableNumber.Equals(-1)) /* return rows from the only available table body */
                {
                    var tableRows = tableTag.FindElements(By.TagName("tr")).ToList();
                    return tableRows;

                }
                /* table contains multiple sub-tables; return rows only from the specified table number */
                var tableRowList =
                    tableTag
                        .FindElements(By.TagName("tbody"))[GlobalVars.TableNumber - 1]
                        .FindElements(By.TagName("tr"))
                        .ToList();
                return tableRowList;
            }
        }

        public static int CountOfTableRows
        {
            get { return GetTableRowsByTagAttribute.Count; }
        }

        public static bool TableRowNumberContainsValue(int tableRowNumber, string value)
        {
            return GetTableRowsByTagAttribute[tableRowNumber - 1].FindElements(By.TagName("td"))
                .Any(t => t.FindElement(By.XPath($"//*[contains(text(), '{value}')]")).Enabled);
        }

        public static bool TableRowNumberColumnNumberContainsValue(int tableRowNumber, int columnNumber, string value)
        {
            /* can simply return td.txt but text can also be enclosed deep within sub-tags*/
            return
                GetTableRowsByTagAttribute[tableRowNumber - 1].FindElements(By.TagName("td"))[columnNumber - 1].Text
                    .Contains(value);
        }

        public static int GetTableRowNumberContainingTheDataInColumnNumber(string data,
            int columnNumber)
        {
            return
                GetTableRowsByTagAttribute.FindIndex(
                    tr =>
                        tr.FindElements(By.TagName("td")).Count >= columnNumber - 1 &&
                        tr.FindElements(By.TagName("td"))[columnNumber - 1].Text.Contains(data));
        }

        public static string GetDataFromTableRowNumberColumnNumber(int rowNumber, int columnNumber)
        {
            return GetTableRowsByTagAttribute[rowNumber - 1].FindElements(By.TagName("td"))[columnNumber - 1].Text;
        }

        public static void ClickColumnNumberInRowNumber(int columnNumber, int rowNumber)
        {
            GetTableRowsByTagAttribute[rowNumber - 1].FindElements(By.TagName("td"))
                [columnNumber - 1].ClickAndWait(3);
        }

        public static void ClickOnLabeledButtonInRowNumber(string label, int rowNumber)
        {
            /* 
            ASSUMPTION: All labeled button have tag="input", value=<input label param> in the specified table row number
            */
            GetTableRowsByTagAttribute[rowNumber - 1].FindElements(By.TagName("input"))
                .Single(t => t.GetAttribute("value").Equals(label))
                .ClickAndWait();
        }

        public static void ClickElementUsingTagAttributeInRowNumber(string tag, string attrType, string attrValue,
            int rowNumber)
            /*
                For row element corresponding to specified row number. In this row, find the tag element whose specified attribute type 
                contains the specified attribute value. Click on this sub-element.
            */
        {
            GetTableRowsByTagAttribute[rowNumber - 1].FindElements(By.TagName(tag))
                .Single(t => t.GetAttribute(attrType).Equals(attrValue)).ClickAndWait();
        }

        public static bool TableContainsData(string data)
        {
            return
                GetTableRowsByTagAttribute.Any(tr => tr.FindElements(By.TagName("td")).Any(td => td.Text.Contains(data)));
        }
    }
}
