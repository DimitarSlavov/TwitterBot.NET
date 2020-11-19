﻿using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbContext.Models
{
    public class LikeAndRetweetSettingsDboModel : MongoBaseModel
    {
		[BsonElement("from_minute")]
		public int FromMinute { get; set; }

		[BsonElement("to_minute")]
		public int ToMinute { get; set; }

		[BsonElement("state")]
		public bool State { get; set; }
	}
}
