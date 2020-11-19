using System.ComponentModel;

namespace CryptoCore.Models.CryptoArticle
{
	public class CryptoArticleModel : ModelBase
	{
		[DisplayName("Article")]
		public FeedArticleModel Article { get; set; }
	}
}
