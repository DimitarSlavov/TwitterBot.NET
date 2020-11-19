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
    public class TwitterModel : PageModel
    {
        private readonly ITwitterAccounts twitterAccounts;

        public TwitterModel(ITwitterAccounts twitterAccounts)
        {
            this.twitterAccounts = twitterAccounts;
        }

        public async Task OnGetAsync()
        {
            AccountsList = await twitterAccounts.GetListAsync() ?? new List<TwitterAccountModel>();
        }

        [BindProperty]
        public TwitterAccountModel Account { get; set; } = new TwitterAccountModel();

        [BindProperty]
        public List<TwitterAccountModel> AccountsList { get; set; }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Account.Name))
                    {
                        await twitterAccounts.CreateAsync(Account);
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
                        await twitterAccounts.DeleteAsync(id);
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