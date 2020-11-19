using CryptoCore.Constants;
using Model = CryptoCore.Models.Twitter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using CryptoCore.Interfaces.Twitter;
using System.Threading.Tasks;
using System;

namespace CryptoNews.Pages.Twitter
{
    [DisplayName("Like And Retweet Settings")]
    [Authorize(Roles = nameof(AuthorizationConstants.Admin))]
    [ValidateAntiForgeryToken]
    public class LikeAndRetweetSettingsModel : PageModel
    {
		private readonly ILikeAndRetweetSettings likeAndRetweetSettings;

        public LikeAndRetweetSettingsModel(ILikeAndRetweetSettings likeAndRetweetSettings)
        {
			this.likeAndRetweetSettings = likeAndRetweetSettings;
		}

		[BindProperty]
		public Model.LikeAndRetweetSettingsModel LikeAndRetweetSettings { get; set; } = new Model.LikeAndRetweetSettingsModel();

		public async Task OnGetAsync()
		{
			try
			{
				var model = await likeAndRetweetSettings.GetSettingsAsync();

				if(model != null)
                {
					LikeAndRetweetSettings = model;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to get Like and Retweet Settings.", ex.InnerException);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				await likeAndRetweetSettings.UpdateSettingsAsync(LikeAndRetweetSettings);
			}

			return RedirectToPage();
		}
	}
}