using CryptoCore.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoNews.ViewComponents
{
	public class CreateAccountViewComponent : ViewComponent
	{
		public CreateAccountViewComponent()
		{

		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.Run(() => View(default(AccountBase)));
		}
	}
}
