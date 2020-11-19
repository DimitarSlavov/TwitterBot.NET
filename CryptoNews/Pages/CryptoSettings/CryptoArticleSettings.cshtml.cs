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
	[DisplayName("Crypto Article")]
	[Authorize(Roles = nameof(AuthorizationConstants.Admin))]
	[ValidateAntiForgeryToken]
	public class CryptoArticleSettingsPageModel : PageModel
	{
		private readonly ICryptoArticleSettings _cryptoArticleSettings;
		private readonly ITwitterStatus _twitterStatus;
		private readonly ICryptoArticle _cryptoArticle;

		public CryptoArticleSettingsPageModel
		(
			ICryptoArticleSettings cryptoArticleSettings,
			ITwitterStatus twitterStatus,
			ICryptoArticle cryptoArticle
		)
		{
			_cryptoArticleSettings = cryptoArticleSettings;
			_twitterStatus = twitterStatus;
			_cryptoArticle = cryptoArticle;
		}

		[BindProperty]
		public CryptoArticleSettingsModel CryptoArticle { get; set; } = new CryptoArticleSettingsModel();

		[BindProperty]
		[DisplayName("Articles Count")]
		public long ArticlesCount { get; set; }

		public void OnGet()
		{
			// to add automapper
			try
			{
				var cryptoArticleSettingsModel = Task.Run(() => _cryptoArticleSettings.GetCryptoArticleSettingsAsync());
				var articles = Task.Run(() => _cryptoArticle.GetCryptoArticleListAsync());

				Task.WaitAll(cryptoArticleSettingsModel, articles);

				if (cryptoArticleSettingsModel.IsCompletedSuccessfully)
					CryptoArticle = cryptoArticleSettingsModel.Result.FirstOrDefault();

				if (articles.IsCompletedSuccessfully)
					ArticlesCount = articles.Result.Count;
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
				try
				{
					if(!string.IsNullOrEmpty(CryptoArticle.CryptoNewsFeed))
						new Uri(CryptoArticle.CryptoNewsFeed);

					await _cryptoArticleSettings.UpdateCryptoArticleSettingsAsync(CryptoArticle);
				}
				catch (Exception)
				{
					//implement logging
				}
			}

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostFeedArticlesAsync()
		{
			try
			{
				await _cryptoArticleSettings.Feed();
			}
			catch (Exception)
			{
				//implement logging
			}

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostSendStatusAsync()
		{
			if (CryptoArticle?.Status?.Length > default(long))
			{
				await Task.Run(() => _twitterStatus.PostStatusToTweeter(CryptoArticle.Status));
			}

			return RedirectToPage();
		}
	}
}