using CryptoCore.Models.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.Accounts
{
    public interface IYouTubeAccounts
    {
        Task CreateAsync(YouTubeAccountModel model);

        Task<List<YouTubeAccountModel>> GetListAsync();

        Task DeleteAsync(string id);
    }
}
