using System;

namespace Twitter
{
	public class Helpers
	{
		public static void ErrMsg(string msg, string e)
		{
			Console.WriteLine($"{msg}: '{e}'\n");
		}
	}
}
