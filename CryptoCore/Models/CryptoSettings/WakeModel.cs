using System.ComponentModel;

namespace CryptoCore.Models.CryptoSettings
{
	public class WakeModel : ModelBase
	{
		[DisplayName("Wake From")]
		public int FromMinute { get; set; }

		[DisplayName("Wake To")]
		public int ToMinute { get; set; }

		[DisplayName("Wake State")]
		public bool State { get; set; }
	}
}
