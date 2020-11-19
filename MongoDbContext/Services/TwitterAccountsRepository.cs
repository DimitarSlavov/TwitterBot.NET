using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class TwitterAccountsRepository : BaseMongoRepository<TwitterAccountDboModel>
	{
		public TwitterAccountsRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
