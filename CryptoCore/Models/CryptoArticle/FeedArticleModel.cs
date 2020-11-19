using System;
using System.Collections.Generic;

namespace CryptoCore.Models.CryptoArticle
{
	public class FeedArticleModel : ModelBase
	{
		public string Title { get; set; }

		public string Link { get; set; }

		public string Description { get; set; }

		public string PublishingDateString { get; set; }

		public DateTime? PublishingDate { get; set; }

		public string Author { get; set; }

		public ICollection<string> Categories { get; set; }

		public string Content { get; set; }
	}
}
