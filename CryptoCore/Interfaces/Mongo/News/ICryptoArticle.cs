using CryptoCore.Models.CryptoArticle;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.News
{
	public interface ICryptoArticle
	{
		Task<List<FeedArticleModel>> GetCryptoArticleListAsync();

		Task<HttpResponseMessage> PostCryptoArticleListAsync(List<CryptoArticleModel> item);
	}
}