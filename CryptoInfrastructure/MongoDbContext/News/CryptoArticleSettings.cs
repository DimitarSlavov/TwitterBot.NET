using CodeHollow.FeedReader;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoArticle;
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
	internal class CryptoArticleSettings : ICryptoArticleSettings
	{
		private readonly ICryptoArticle _cryptoArticle;
		private readonly CryptoArticleSettingsRepository _cryptoArticleSettingsRepository;

		public CryptoArticleSettings
		(
			ICryptoArticle cryptoArticle,
			CryptoArticleSettingsRepository cryptoArticleSettingsRepository
		)
		{
			_cryptoArticle = cryptoArticle;
			_cryptoArticleSettingsRepository = cryptoArticleSettingsRepository;
		}

		public async Task<bool> GetStateAsync()
		{
			var result = await this.GetCryptoArticleSettingsAsync();

			return result.First().State;
		}

		public async Task<double> GetFeedMinutesAsync()
		{
			var result = await this.GetCryptoArticleSettingsAsync();

			return result.First().FeedMinutes;
		}

		public async Task Feed()
		{
			var modelList = await this.GetCryptoArticleSettingsAsync();

			var model = modelList.FirstOrDefault();

			List<CryptoArticleModel> articles = new List<CryptoArticleModel>();

			foreach (string feedItem in model?.CryptoNewsFeeds)
			{
				Feed feed = await FeedReader.ReadAsync(feedItem);

				if (feed?.Items?.Count != default)
					foreach (var article in feed.Items)
					{
						articles.Add(new CryptoArticleModel
						{
							Article = CommonHelper.ModelMapper<FeedItem, FeedArticleModel>(article)
						});
					}
			}

			if (articles.Count > default(long))
				await _cryptoArticle.PostCryptoArticleListAsync(articles);
		}

		public async Task<List<CryptoArticleSettingsModel>> GetCryptoArticleSettingsAsync()
		{
			var articleSettingsDbo = await _cryptoArticleSettingsRepository.GetListAsync();

			return CommonHelper.ModelMapper<List<CryptoArticleSettingsDboModel>, List<CryptoArticleSettingsModel>>(articleSettingsDbo);
		}

		public async Task<HttpResponseMessage> UpdateCryptoArticleSettingsAsync(CryptoArticleSettingsModel item)
		{
			var cryptoArticleSettings = _cryptoArticleSettingsRepository.GetByIdAsync(item.Id);
			Task.WaitAll(cryptoArticleSettings);

			if (cryptoArticleSettings == null)
				return new HttpResponseMessage(HttpStatusCode.NotFound);

			var result = cryptoArticleSettings.Result;

			result.FeedMinutes = item.FeedMinutes;
			result.State = item.State;

			if (item.CryptoNewsFeeds != null)
				result.CryptoNewsFeeds = item.CryptoNewsFeeds
					.Where(i => i != null)
					.ToList();

			if (!string.IsNullOrEmpty(item.CryptoNewsFeed))
				if (item.CryptoNewsFeeds == null)
					result.CryptoNewsFeeds = new List<string>() { item.CryptoNewsFeed };
				else
					result.CryptoNewsFeeds.Add(item.CryptoNewsFeed);

			await _cryptoArticleSettingsRepository.UpdateAsync(item.Id, result);

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}
	}
}
