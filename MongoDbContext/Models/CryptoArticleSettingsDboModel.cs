using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDbContext.Models
{
	public class CryptoArticleSettingsDboModel : MongoBaseModel
	{
		[BsonElement("feed_minutes")]
		public double FeedMinutes { get; set; }

		[BsonElement("crypto_news_feeds")]
		public List<string> CryptoNewsFeeds { get; set; }

		[BsonElement("state")]
		public bool State { get; set; }
	}
}
