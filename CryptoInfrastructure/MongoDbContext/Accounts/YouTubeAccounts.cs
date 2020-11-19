using CryptoCore.Interfaces.Mongo.Accounts;
using CryptoCore.Models.Accounts;
using CryptoInfrastructure.Helpers;
using MongoDbContext.Models;
using MongoDbContext.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoInfrastructure.MongoDbContext.Accounts
{
    internal class YouTubeAccounts : IYouTubeAccounts
    {
        private readonly YouTubeAccountsRepository youTubeAccountsRepository;

        public YouTubeAccounts(YouTubeAccountsRepository youTubeAccountsRepository)
        {
            this.youTubeAccountsRepository = youTubeAccountsRepository;
        }

        public async Task CreateAsync(YouTubeAccountModel model)
        {
            var dboModel = CommonHelper.ModelMapper<YouTubeAccountModel, YouTubeAccountDboModel>(model);

            await youTubeAccountsRepository.CreateAsync(dboModel);
        }

        public async Task<List<YouTubeAccountModel>> GetListAsync()
        {
            var accounts = await youTubeAccountsRepository.GetListAsync();

            var result = default(List<YouTubeAccountModel>);

            if (accounts != null && accounts.Any())
            {
                result = CommonHelper.ModelMapper<List<YouTubeAccountDboModel>, List<YouTubeAccountModel>>(accounts);
            }
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            await youTubeAccountsRepository.DeleteAsync(id);
        }
    }
}
