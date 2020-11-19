using System.ComponentModel;

namespace CryptoCore.Models.CryptoSettings
{
	public class TwitterFollowUserModel : ModelBase
	{
		[DisplayName("Follow From")]
		public int FromMinute { get; set; }

		[DisplayName("Follow To")]
		public int ToMinute { get; set; }

		[DisplayName("Follow State")]
		public bool State { get; set; }
	}
}
