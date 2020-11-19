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
using Twitter;

namespace CryptoInfrastructure.MongoDbContext.News
{
	internal class TwitterFollowUser : ITwitterFollowUser
	{
		private readonly TwitterFollowUserRepository _twitterFollowUserRepository;

		public TwitterFollowUser
		(
			TwitterFollowUserRepository twitterFollowUserRepository
		)
		{
			_twitterFollowUserRepository = twitterFollowUserRepository;
		}

		public async Task<bool> GetStateAsync()
		{
			var result = await this.GetTwitterFollowAsync();

			return result.First().State;
		}

		public async Task<double> GenerateRandomAsync()
		{
			var result = await this.GetTwitterFollowAsync();

			return CommonHelper.RandomNumber(result.First().FromMinute, result.First().ToMinute);
		}

		public async Task<List<TwitterFollowUserModel>> GetTwitterFollowAsync()
		{
			var wakeDbo = await _twitterFollowUserRepository.GetListAsync();

			return CommonHelper.ModelMapper<List<TwitterFollowUserDboModel>, List<TwitterFollowUserModel>>(wakeDbo);
		}

		public async Task<HttpResponseMessage> UpdateTwitterFollowAsync(TwitterFollowUserModel item)
		{
			var wake = _twitterFollowUserRepository.GetByIdAsync(item.Id);
			Task.WaitAll(wake);

			if (wake == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var result = CommonHelper.ModelMapper<TwitterFollowUserModel, TwitterFollowUserDboModel>(item);

			await _twitterFollowUserRepository.UpdateAsync(item.Id, result);

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
