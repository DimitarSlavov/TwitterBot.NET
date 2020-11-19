using CryptoCore.Models.Accounts;
using CryptoCore.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoNews.ViewComponents
{
	public class ListTwitterAccountsViewComponent : ViewComponent
	{
		public ListTwitterAccountsViewComponent()
		{

		}

		public async Task<IViewComponentResult> InvokeAsync(IList<TwitterAccountModel> items)
		{
			return await Task.Run(() => View(items));
		}
	}
}
