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
    internal class TwitterAccounts : ITwitterAccounts
    {
        private readonly TwitterAccountsRepository twitterAccountsRepository;

        public TwitterAccounts(TwitterAccountsRepository twitterAccountsRepository)
        {
            this.twitterAccountsRepository = twitterAccountsRepository;
        }

        public async Task CreateAsync(TwitterAccountModel model)
        {
            var dboModel = CommonHelper.ModelMapper<TwitterAccountModel, TwitterAccountDboModel>(model);

            await twitterAccountsRepository.CreateAsync(dboModel);
        }

        public async Task<List<TwitterAccountModel>> GetListAsync()
        {
            var accounts = await twitterAccountsRepository.GetListAsync();

            var result = default(List<TwitterAccountModel>);

            if (accounts != null && accounts.Any())
            {
                result = CommonHelper.ModelMapper<List<TwitterAccountDboModel>, List<TwitterAccountModel>>(accounts);
            }
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            await twitterAccountsRepository.DeleteAsync(id);
        }
    }
}
