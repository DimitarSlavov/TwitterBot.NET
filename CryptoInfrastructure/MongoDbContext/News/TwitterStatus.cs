using CryptoCore.Constants;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoArticle;
using CryptoCore.Models.CryptoSettings;
using CryptoInfrastructure.Helpers;
using MongoDbContext.Models;
using MongoDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twitter;

namespace CryptoInfrastructure.MongoDbContext.News
{
	internal class TwitterStatus : ITwitterStatus
	{
		private readonly ICryptoArticle cryptoArticle;
		private readonly TwitterStatusRepository twitterStatusRepository;
		private readonly ITwitterActions twitterActions;

		public TwitterStatus
		(
			ICryptoArticle cryptoArticle,
			TwitterStatusRepository twitterStatusRepository,
			ITwitterActions twitterActions
		)
		{
			this.cryptoArticle = cryptoArticle;
			this.twitterStatusRepository = twitterStatusRepository;
			this.twitterActions = twitterActions;
		}

		private IList<FeedArticleModel> Articles { get; set; } = new List<FeedArticleModel>();

		public async Task<bool> GetStateAsync()
		{
			var result = await this.GetTwitterStatusAsync();

			return result.First().State;
		}

		public async Task TweetArticlesAsStatus()
		{
			Articles = await cryptoArticle.GetCryptoArticleListAsync();

			if (Articles.Count > default(int))
				await Task.Run(() => PostStatusToTweeter());
		}

		public void PostStatusToTweeter(string status) =>
			twitterActions.PublishTweet(status);

		public async Task<double> GenerateRandomAsync()
		{
			var result = await this.GetTwitterStatusAsync();

			return CommonHelper.RandomNumber(result.First().FromMinute, result.First().ToMinute);
		}

		public async Task<List<TwitterStatusModel>> GetTwitterStatusAsync()
		{
			var twitterDbo = await twitterStatusRepository.GetListAsync();

			return CommonHelper.ModelMapper<List<TwitterStatusDboModel>, List<TwitterStatusModel>>(twitterDbo);
		}

		public async Task<HttpResponseMessage> UpdateTwitterStatusAsync(TwitterStatusModel item)
		{
			var twitterStatus = twitterStatusRepository.GetByIdAsync(item.Id);
			Task.WaitAll(twitterStatus);

			if (twitterStatus == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var result = CommonHelper.ModelMapper<TwitterStatusModel, TwitterStatusDboModel>(item);

			await twitterStatusRepository.UpdateAsync(item.Id, result);

			return new HttpResponseMessage(HttpStatusCode.NoContent);
		}

		#region Helpers

		private void PostStatusToTweeter()
		{
			FeedArticleModel article = GetRandomArticle();

			string categories = ParsedCategories(article);

			string status =
				$"{ article.Title } " +
				$"{ categories } " +
				$"{ article.Link }";

			PostStatusToTweeter(status);
		}

		private string ParsedCategories(FeedArticleModel article)
		{
			StringBuilder categories = new StringBuilder();

			long counter = default;

			foreach (var category in article.Categories)
			{
				if (article.Title.Length +
					article.Link.Length +
					categories.Length >= TwitterConstants.TweetLength ||
					counter == 3)
				//to make 3 CategoriesCounter Param
				{
					break;
				}
				//removes numbers from hashtags
				Regex regex = new Regex("[^A-z\\d]");

				var tag = regex.Replace(category, string.Empty);

				categories.Append($" {TwitterConstants.HashTag}{tag} ");
				counter++;
			}

			return categories.ToString();
		}

		private FeedArticleModel GetRandomArticle()
		{
			var randomArticle = new Random().Next(default, Articles.Count);

			FeedArticleModel article = Articles[randomArticle];

			Articles.RemoveAt(randomArticle);

			return article;
		}

		#endregion
	}
}
