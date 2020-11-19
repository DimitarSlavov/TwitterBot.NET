using System.ComponentModel;

namespace CryptoCore.Models.Twitter
{
	public class LikeAndRetweetSettingsModel : ModelBase
	{
		[DisplayName("Like And Retweet From")]
		public int FromMinute { get; set; }

		[DisplayName("Like And Retweet To")]
		public int ToMinute { get; set; }

		[DisplayName("Like And Retweet State")]
		public bool State { get; set; }
	}
}
