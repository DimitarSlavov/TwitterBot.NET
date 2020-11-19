namespace CryptoCore.Constants
{
    public static class AuthorizationConstants
	{
        public static string Admin;
        public static string User;
		public static string AdminUsername { get; private set; }

		public static void SetAdminUsername(string name)
		{
			AdminUsername = name;
		}
	}
}
