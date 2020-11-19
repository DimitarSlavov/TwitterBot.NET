using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CryptoCore.Constants;
using CryptoCore.Interfaces.Mongo.Accounts;
using CryptoCore.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CryptoNews.Pages.Accounts
{
    [DisplayName("Twitter Accounts")]
    [Authorize(Roles = nameof(AuthorizationConstants.Admin))]
    [ValidateAntiForgeryToken]
    public class YouTubeModel : PageModel
    {
        private readonly IYouTubeAccounts youTubeAccounts;

        public YouTubeModel(IYouTubeAccounts youTubeAccounts)
        {
            this.youTubeAccounts = youTubeAccounts;
        }

        public async Task OnGetAsync()
        {
            AccountsList = await youTubeAccounts.GetListAsync() ?? new List<YouTubeAccountModel>();
        }

        [BindProperty]
        public YouTubeAccountModel Account { get; set; } = new YouTubeAccountModel();

        [BindProperty]
        public List<YouTubeAccountModel> AccountsList { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Account.Name))
                    {
                        await youTubeAccounts.CreateAsync(Account);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to add Twitter Account", ex.InnerException);
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        await youTubeAccounts.DeleteAsync(id);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete Twitter Account", ex.InnerException);
                }
            }

            return RedirectToPage();
        }
    }
}