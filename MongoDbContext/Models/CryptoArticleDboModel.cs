using CryptoCore.Models.CryptoArticle;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbContext.Models
{
	public class CryptoArticleDboModel : MongoBaseModel
	{
		[BsonElement("articles")]
		public FeedArticleModel Article { get; set; }
	}
}
