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
	[DisplayName("Unfollow Twitter User")]
	[Authorize(Roles = nameof(AuthorizationConstants.Admin))]
	[ValidateAntiForgeryToken]
	public class TwitterUnfollowUserPageModel : PageModel
	{
		private readonly ITwitterUnfollowUser _twitterUnfollowUser;

		public TwitterUnfollowUserPageModel
		(
			ITwitterUnfollowUser twitterUnfollowUser
		)
		{
			_twitterUnfollowUser = twitterUnfollowUser;
		}

		[BindProperty]
		public TwitterUnfollowUserModel TwitterUnfollowUser { get; set; } = new TwitterUnfollowUserModel();

		public void OnGet()
		{
			// to add automapper
			try
			{
				var item = Task.Run(() => _twitterUnfollowUser.GetTwitterUnfollowAsync());

				Task.WaitAll(item);

				if (item.IsCompletedSuccessfully)
					TwitterUnfollowUser = item.Result.FirstOrDefault();
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
				await _twitterUnfollowUser.UpdateTwitterUnfollowAsync(TwitterUnfollowUser);
			}

			return RedirectToPage();
		}
	}
}