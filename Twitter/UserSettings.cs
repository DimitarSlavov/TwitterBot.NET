using Tweetinvi;

namespace Twitter
{
    internal class UserSettings
    {
		private string consumerKey;
		private string consumerSecret;
		private string userAccessToken;
		private string userAccessSecret;

		public static long AccountId { get; private set; }

		public UserSettings(
			string consumerKey,
			string consumerSecret,
			string userAccessToken,
			string userAccessSecret)
		{
			this.consumerKey = consumerKey;
			this.consumerSecret = consumerSecret;
			this.userAccessToken = userAccessToken;
			this.userAccessSecret = userAccessSecret;
		}

		public void Authenticate()
		{
			Auth.SetUserCredentials
			(
				consumerKey,
				consumerSecret,
				userAccessToken,
				userAccessSecret
			);
		}

		public void SetAdditionalParams(
			long accountId)
		{
			AccountId = accountId;
		}
	}
}
