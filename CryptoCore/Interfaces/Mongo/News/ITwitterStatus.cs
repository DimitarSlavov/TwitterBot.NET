using CryptoCore.Interfaces.Common;
using CryptoCore.Models.CryptoSettings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.News
{
	public interface ITwitterStatus : IBackgroundService
	{
		Task TweetArticlesAsStatus();

		void PostStatusToTweeter(string status);

		Task<List<TwitterStatusModel>> GetTwitterStatusAsync();

		Task<HttpResponseMessage> UpdateTwitterStatusAsync(TwitterStatusModel item);
	}
}