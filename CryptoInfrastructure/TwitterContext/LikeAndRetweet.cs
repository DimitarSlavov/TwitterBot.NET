using CryptoCore.Interfaces.Twitter;
using MongoDbContext.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Twitter;

namespace CryptoInfrastructure.TwitterContext
{
    internal class LikeAndRetweet : ILikeAndRetweet
    {
        private readonly TwitterAccountsRepository twitterAccountsRepository;
        private readonly ITwitterActions twitterActions;

        public LikeAndRetweet(
            TwitterAccountsRepository twitterAccountsRepository,
            ITwitterActions twitterActions)
        {
            this.twitterAccountsRepository = twitterAccountsRepository;
            this.twitterActions = twitterActions;
        }

        public async Task<bool> LikeAndRetweetWhitelistedAccount()
        {
            try
            {
                var whitelistedAccounts = await twitterAccountsRepository.GetListAsync();
                var accounts = whitelistedAccounts.Select(e => e.Name);
                var randomAccount = accounts.ElementAt(new Random().Next(0, accounts.Count() - 1));
                var success = twitterActions.LikeAndRetweetLastTweet(randomAccount);

                if (success != null && success.Value == true)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to like and retweet.", ex.InnerException);
            }
        }
    }
}
