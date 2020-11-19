using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class TwitterUnfollowUserRepository : BaseMongoRepository<TwitterUnfollowUserDboModel>
	{
		public TwitterUnfollowUserRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
