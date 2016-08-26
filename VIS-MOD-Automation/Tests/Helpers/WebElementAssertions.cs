using FluentAssertions;
using OpenQA.Selenium;

namespace Tests.Helpers
{
	static class WebElementAssertions
	{
		public static void ShouldContainText(this IWebElement element, string text)
		{
			element.Displayed.Should().BeTrue();
			element.Text.ToLower().Should().Contain(text.ToLower());
		}
	}
}