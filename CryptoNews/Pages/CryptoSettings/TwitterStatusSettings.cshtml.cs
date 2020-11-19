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

namespace CryptoNews.Pages.CryptoSettings
{
	[DisplayName("Twitter Status")]
	[Authorize(Roles = nameof(AuthorizationConstants.Admin))]
	[ValidateAntiForgeryToken]
	public class TwitterStatusSettingsPageModel : PageModel
	{
		private readonly ILogger _logger;
		private readonly ITwitterStatus _twitterStatus;

		public TwitterStatusSettingsPageModel
		(
			ILogger<TwitterStatusSettingsPageModel> logger,
			ITwitterStatus twitterStatus
		)
		{
			_logger = logger;
			_twitterStatus = twitterStatus;
		}

		[BindProperty]
		public TwitterStatusModel TwitterStatus { get; set; } = new TwitterStatusModel();

		public void OnGet()
		{
			// to add automapper
			try
			{
				var twitterStatusModel = Task.Run(() => _twitterStatus.GetTwitterStatusAsync());

				Task.WaitAll(twitterStatusModel);

				if (twitterStatusModel.IsCompletedSuccessfully)
					TwitterStatus = twitterStatusModel.Result.FirstOrDefault();
			}
			catch (Exception)
			{
				//implement logging
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			dynamic result = Page();

			if (ModelState.IsValid)
			{
				await _twitterStatus.UpdateTwitterStatusAsync(TwitterStatus);

				result = RedirectToPage();
			}

			return result;
		}

		public async Task<IActionResult> OnPostSendRandomStatusAsync()
		{
			await _twitterStatus.TweetArticlesAsStatus();

			return RedirectToPage();
		}
	}
}