using CryptoCore.Constants;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Pages.Manage
{
	[DisplayName("Wake Me")]
	[Authorize(Roles = nameof(AuthorizationConstants.Admin))]
	[ValidateAntiForgeryToken]
	public class WakeSettingsPageModel : PageModel
	{
		private readonly IWake _wake;

		public WakeSettingsPageModel
		(
			IWake wake
		)
		{
			_wake = wake;
		}

		[BindProperty]
		public WakeModel Wake { get; set; } = new WakeModel();

		public void OnGet()
		{
			// to add automapper
			try
			{
				var wakeModel = Task.Run(() => _wake.GetWakeAsync());

				Task.WaitAll(wakeModel);

				if (wakeModel.IsCompletedSuccessfully)
					Wake = wakeModel.Result.FirstOrDefault();
			}
			catch (Exception)
			{
				//implement logging
			}
		}

		public async Task<IActionResult> OnPostWakeAsync()
		{
			dynamic result = Page();

			if (ModelState.IsValid)
			{
				await _wake.UpdateWakeAsync(Wake);

				result = RedirectToPage();
			}

			return result;
		}
	}
}