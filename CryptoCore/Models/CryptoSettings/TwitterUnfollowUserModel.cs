using System.ComponentModel;

namespace CryptoCore.Models.CryptoSettings
{
	public class TwitterUnfollowUserModel : ModelBase
	{
		[DisplayName("Unfollow From")]
		public int FromMinute { get; set; }

		[DisplayName("Unfollow To")]
		public int ToMinute { get; set; }

		[DisplayName("Unfollow State")]
		public bool State { get; set; }
	}
}
