using CryptoCore.Models.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.Accounts
{
    public interface ITwitterAccounts
    {
        Task CreateAsync(TwitterAccountModel model);

        Task<List<TwitterAccountModel>> GetListAsync();

        Task DeleteAsync(string id);
    }
}
