using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CryptoNews.Pages
{
	public class IndexModel : PageModel
	{
		public async Task OnGetAsync()
		{
			await Task.Run(() => { });
		}
	}
}