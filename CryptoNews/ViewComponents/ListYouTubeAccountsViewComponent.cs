using CryptoCore.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoNews.ViewComponents
{
	public class ListYouTubeAccountsViewComponent : ViewComponent
	{
		public ListYouTubeAccountsViewComponent()
		{

		}

		public async Task<IViewComponentResult> InvokeAsync(IList<YouTubeAccountModel> items)
		{
			return await Task.Run(() => View(items));
		}
	}
}
