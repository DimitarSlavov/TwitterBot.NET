using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class LikeAndRetweetSettingsRepository : BaseMongoRepository<LikeAndRetweetSettingsDboModel>
	{
		public LikeAndRetweetSettingsRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
