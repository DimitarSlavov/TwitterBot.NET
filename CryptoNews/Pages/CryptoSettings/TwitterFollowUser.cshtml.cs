using CryptoCore.Constants;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Models.CryptoSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Pages.CryptoSettings
{
	[DisplayName("Follow Twitter User")]
	[Authorize(Roles = nameof(AuthorizationConstants.Admin))]
	[ValidateAntiForgeryToken]
	public class TwitterFollowUserPageModel : PageModel
	{
		private readonly ITwitterFollowUser _twitterFollowUser;

		public TwitterFollowUserPageModel
		(
			ITwitterFollowUser twitterFollowUser
		)
		{
			_twitterFollowUser = twitterFollowUser;
		}

		[BindProperty]
		public TwitterFollowUserModel TwitterFollowUser { get; set; } = new TwitterFollowUserModel();

		public void OnGet()
		{
			// to add automapper
			try
			{
				var item = Task.Run(() => _twitterFollowUser.GetTwitterFollowAsync());

				Task.WaitAll(item);

				if (item.IsCompletedSuccessfully)
					TwitterFollowUser = item.Result.FirstOrDefault();
			}
			catch (Exception)
			{
				//implement logging
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				await _twitterFollowUser.UpdateTwitterFollowAsync(TwitterFollowUser);
			}

			return RedirectToPage();
		}
	}
}