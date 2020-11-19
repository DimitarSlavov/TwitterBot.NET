using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class CryptoArticleSettingsRepository : BaseMongoRepository<CryptoArticleSettingsDboModel>
	{
		public CryptoArticleSettingsRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
