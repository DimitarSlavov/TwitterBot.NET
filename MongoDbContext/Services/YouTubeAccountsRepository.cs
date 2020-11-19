using CryptoCore.Models.Settings;
using MongoDbContext.Models;

namespace MongoDbContext.Services
{
    public class YouTubeAccountsRepository : BaseMongoRepository<YouTubeAccountDboModel>
    {
        public YouTubeAccountsRepository(DbSettings settings) : base(settings)
        {
        }
    }
}
