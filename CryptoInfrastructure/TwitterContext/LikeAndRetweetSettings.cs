using CryptoCore.Interfaces.Twitter;
using CryptoCore.Models.Twitter;
using CryptoInfrastructure.Helpers;
using MongoDbContext.Models;
using MongoDbContext.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoInfrastructure.TwitterContext
{
    internal class LikeAndRetweetSettings : ILikeAndRetweetSettings
    {
        private readonly LikeAndRetweetSettingsRepository likeAndRetweetSettingsRepository;

        public LikeAndRetweetSettings(LikeAndRetweetSettingsRepository likeAndRetweetSettingsRepository)
        {
            this.likeAndRetweetSettingsRepository = likeAndRetweetSettingsRepository;
        }

        public async Task<bool> GetStateAsync()
        {
            var result = await GetSettingsAsync();

            return result.State;
        }

        public async Task<double> GenerateRandomAsync()
        {
            var result = await GetSettingsAsync();

            return CommonHelper.RandomNumber(result.FromMinute, result.ToMinute);
        }

        public async Task<LikeAndRetweetSettingsModel> GetSettingsAsync()
        {
            var item = await likeAndRetweetSettingsRepository.GetFirstOrDefaultAsync();

            return CommonHelper.ModelMapper<LikeAndRetweetSettingsDboModel, LikeAndRetweetSettingsModel>(item);
        }

        public async Task<HttpResponseMessage> UpdateSettingsAsync(LikeAndRetweetSettingsModel model)
        {
            var item = await likeAndRetweetSettingsRepository.GetByIdAsync(model.Id);

            if (item == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var result = CommonHelper.ModelMapper<LikeAndRetweetSettingsModel, LikeAndRetweetSettingsDboModel>(model);

            await likeAndRetweetSettingsRepository.UpdateAsync(model.Id, result);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
