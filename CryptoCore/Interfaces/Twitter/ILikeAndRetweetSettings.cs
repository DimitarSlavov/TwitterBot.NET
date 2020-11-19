using CryptoCore.Interfaces.Common;
using CryptoCore.Models.Twitter;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Twitter
{
    public interface ILikeAndRetweetSettings : IBackgroundService
    {
        Task<LikeAndRetweetSettingsModel> GetSettingsAsync();

        Task<HttpResponseMessage> UpdateSettingsAsync(LikeAndRetweetSettingsModel model);
    }
}
