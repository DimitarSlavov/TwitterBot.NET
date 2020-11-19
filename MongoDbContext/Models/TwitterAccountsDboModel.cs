using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbContext.Models
{
    public class TwitterAccountDboModel : MongoBaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
