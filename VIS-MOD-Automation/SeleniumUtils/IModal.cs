namespace SeleniumUtils
{
	public interface IModal
	{
		string Title { get; }
		bool IsDisplayed { get; }
		void Ok();
		TPage Ok<TPage>(string expectedPageTitle = null, double timeoutSec = 10) where TPage : IPage, new();
		void Close();
	}
}