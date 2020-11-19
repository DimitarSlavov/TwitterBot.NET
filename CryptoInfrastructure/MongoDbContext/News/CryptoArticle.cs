using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoArticle;
using CryptoInfrastructure.Helpers;
using MongoDB.Driver;
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
	internal class CryptoArticle : ICryptoArticle
	{
		private readonly CryptoArticleRepository _cryptoArticleRepository;

		public CryptoArticle
		(
			CryptoArticleRepository cryptoArticleRepository
		)
		{
			_cryptoArticleRepository = cryptoArticleRepository;
		}

		public async Task<List<FeedArticleModel>> GetCryptoArticleListAsync()
		{
			var artilesDbo = await _cryptoArticleRepository.GetListAsync();

			List<FeedArticleModel> result = new List<FeedArticleModel>();

			artilesDbo.ForEach(a => result.Add(a.Article));

			return result;
		}

		public async Task<HttpResponseMessage> PostCryptoArticleListAsync(List<CryptoArticleModel> items)
		{
			HttpStatusCode result = HttpStatusCode.BadRequest;

			try
			{
				IList<CryptoArticleDboModel> dbArticles = await _cryptoArticleRepository.GetListAsync();

				var courses = items.ToList();

				courses.RemoveAll(a =>
				{
					bool res = false;

					foreach (var item in dbArticles)
					{
						if (a.Article.Id == item.Article.Id ||
							a.Article.PublishingDate.Value.Day != DateTime.Now.Day)
						{
							res = true;
							break;
						}
					}

					return res;
				});

				DeleteResult deleteArticles = await _cryptoArticleRepository.DeleteManyAsync();

				if (courses.Any())
				{
					var itemsDbo = CommonHelper.ModelMapper<List<CryptoArticleModel>, List<CryptoArticleDboModel>>(courses);
					await _cryptoArticleRepository.InsertManyAsync(itemsDbo);
				}

				result = HttpStatusCode.OK;
			}
			catch (Exception)
			{
				//implement logging
			}

			return new HttpResponseMessage(result);
		}
	}
}
