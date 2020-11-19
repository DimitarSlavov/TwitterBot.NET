using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CryptoCore.Models.CryptoSettings
{
	public class CryptoArticleSettingsModel : ModelBase
	{
		[DisplayName("Feed Minutes")]
		[Range(0, 1800)]
		public double FeedMinutes { get; set; }

		[DisplayName("Articles State")]
		public bool State { get; set; }

		[DisplayName("Crypto News Feeds")]
		public IList<string> CryptoNewsFeeds { get; set; }

		[DisplayName("Add Feed")]
		public string CryptoNewsFeed { get; set; }

		[DisplayName("Tweet Text")]
		[MaxLength(280)]
		public string Status { get; set; }
	}
}
