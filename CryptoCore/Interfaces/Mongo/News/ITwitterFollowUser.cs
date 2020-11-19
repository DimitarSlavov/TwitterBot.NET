using CryptoCore.Interfaces.Common;
using CryptoCore.Models.CryptoSettings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.News
{
	public interface ITwitterFollowUser : IBackgroundService
	{
		Task<List<TwitterFollowUserModel>> GetTwitterFollowAsync();

		Task<HttpResponseMessage> UpdateTwitterFollowAsync(TwitterFollowUserModel item);
	}
}
