namespace CryptoCore.Constants
{
	public class EnvironmentConstants
	{
		public static string CurrentEnvironment { get; private set; }

		public static string Development;

		public static void SetCurrentEnvironment(string env)
		{
			CurrentEnvironment = env;
		}
	}
}
