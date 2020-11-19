using CryptoCore.Constants;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoSettings;
using CryptoInfrastructure.Helpers;
using MongoDbContext.Models;
using MongoDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoInfrastructure.MongoDbContext.News
{
	internal class Wake : IWake
	{
		private readonly HttpClient httpClient;
		private readonly WakeRepository wakeRepository;

		public Wake(
			WakeRepository wakeRepository)
		{
			httpClient = new HttpClient();
			this.wakeRepository = wakeRepository;
		}

		public async Task WakeMe()
		{
			try
			{
				await httpClient.GetAsync(HerokuConstants.HerokuUri);
			}
			catch (Exception ex)
			{
				throw new Exception("Error while request was executed", ex.InnerException);
			}
		}

		public async Task<bool> GetStateAsync()
		{
			var result = await this.GetWakeAsync();

			return result.First().State;
		}

		public async Task<double> GenerateRandomAsync()
		{
			var result = await this.GetWakeAsync();

			return CommonHelper.RandomNumber(result.First().FromMinute, result.First().ToMinute);
		}
			
		public async Task<List<WakeModel>> GetWakeAsync()
		{
			var wakeDbo = await wakeRepository.GetListAsync();

			return CommonHelper.ModelMapper<List<WakeDboModel>, List<WakeModel>>(wakeDbo);
		}

		public async Task<HttpResponseMessage> UpdateWakeAsync(WakeModel item)
		{
			var wake = wakeRepository.GetByIdAsync(item.Id);
			Task.WaitAll(wake);

			if (wake == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var result = CommonHelper.ModelMapper<WakeModel, WakeDboModel>(item);

			await wakeRepository.UpdateAsync(item.Id, result);

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}

