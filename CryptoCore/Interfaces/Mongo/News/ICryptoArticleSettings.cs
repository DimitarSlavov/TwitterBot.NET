using CryptoCore.Interfaces.Common;
using CryptoCore.Models.CryptoSettings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.News
{
	public interface ICryptoArticleSettings : IGetState
	{
		Task Feed();

		Task<List<CryptoArticleSettingsModel>> GetCryptoArticleSettingsAsync();

		Task<HttpResponseMessage> UpdateCryptoArticleSettingsAsync(CryptoArticleSettingsModel item);

		Task<double> GetFeedMinutesAsync();
	}
}