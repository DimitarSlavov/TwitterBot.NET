using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
	public class CryptoArticleRepository : BaseMongoRepository<CryptoArticleDboModel>
	{
		public CryptoArticleRepository(DbSettings settings) : base(settings)
		{
		}
	}
}
