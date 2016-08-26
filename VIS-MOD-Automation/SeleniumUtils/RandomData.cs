using System;

namespace SeleniumUtils
{
	public static class RandomData
	{
		public static int Generate(int maxValue = 1000000)
		{
			var random = new Random();
			return random.Next(maxValue);
		}
	}
}