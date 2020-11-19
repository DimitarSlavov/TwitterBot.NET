using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class TwitterStatusRepository : BaseMongoRepository<TwitterStatusDboModel>
	{
		public TwitterStatusRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
