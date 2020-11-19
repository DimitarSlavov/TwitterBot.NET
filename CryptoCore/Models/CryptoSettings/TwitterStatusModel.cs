using System.ComponentModel;

namespace CryptoCore.Models.CryptoSettings
{
	public class TwitterStatusModel : ModelBase
	{
		[DisplayName("Tweet From")]
		public int FromMinute { get; set; }

		[DisplayName("Tweet To")]
		public int ToMinute { get; set; }

		[DisplayName("Twitter State")]
		public bool State { get; set; }
	}
}
