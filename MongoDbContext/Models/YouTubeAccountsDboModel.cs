using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbContext.Models
{
    public class YouTubeAccountDboModel : MongoBaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
