using CryptoCore.Interfaces.Common;
using CryptoCore.Models.CryptoSettings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCore.Interfaces.Mongo.News
{
	public interface IWake : IBackgroundService
	{
		Task WakeMe();

		Task<List<WakeModel>> GetWakeAsync();

		Task<HttpResponseMessage> UpdateWakeAsync(WakeModel item);
	}
}