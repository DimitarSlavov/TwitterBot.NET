using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class WakeRepository : BaseMongoRepository<WakeDboModel>
	{
		public WakeRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
