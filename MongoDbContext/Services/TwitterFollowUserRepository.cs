using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class TwitterFollowUserRepository : BaseMongoRepository<TwitterFollowUserDboModel>
	{
		public TwitterFollowUserRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
