using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoSettings;
using CryptoInfrastructure.Helpers;
using MongoDbContext.Models;
using MongoDbContext.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoInfrastructure.MongoDbContext.News
{
    internal class TwitterUnfollowUser : ITwitterUnfollowUser
    {
        private readonly TwitterUnfollowUserRepository twitterUnfollowUserRepository;

        public TwitterUnfollowUser(TwitterUnfollowUserRepository twitterUnfollowUserRepository)
        {
            this.twitterUnfollowUserRepository = twitterUnfollowUserRepository;
        }

        public async Task<bool> GetStateAsync()
        {
            var result = await this.GetTwitterUnfollowAsync();

            return result.First().State;
        }

        public async Task<double> GenerateRandomAsync()
        {
            var result = await this.GetTwitterUnfollowAsync();

            return CommonHelper.RandomNumber(result.First().FromMinute, result.First().ToMinute);
        }

        public async Task<List<TwitterUnfollowUserModel>> GetTwitterUnfollowAsync()
        {
            var item = await twitterUnfollowUserRepository.GetListAsync();

            return CommonHelper.ModelMapper<List<TwitterUnfollowUserDboModel>, List<TwitterUnfollowUserModel>>(item);
        }

        public async Task<HttpResponseMessage> UpdateTwitterUnfollowAsync(TwitterUnfollowUserModel item)
        {
            var wake = twitterUnfollowUserRepository.GetByIdAsync(item.Id);
            Task.WaitAll(wake);

            if (wake == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var result = CommonHelper.ModelMapper<TwitterUnfollowUserModel, TwitterUnfollowUserDboModel>(item);

            await twitterUnfollowUserRepository.UpdateAsync(item.Id, result);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
