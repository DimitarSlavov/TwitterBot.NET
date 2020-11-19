using CryptoCore.Interfaces.Mongo.News;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Twitter;

namespace CryptoInfrastructure.Services
{
	internal class TwitterUnFollowUserService : BackgroundService
	{
		private readonly ITwitterUnfollowUser twitterUnfollowUser;
		private readonly ITwitterActions twitterActions;

		public TwitterUnFollowUserService(
			ITwitterUnfollowUser twitterUnfollowUser,
			ITwitterActions twitterActions)
		{
			this.twitterUnfollowUser = twitterUnfollowUser;
			this.twitterActions = twitterActions;
		}

		protected override async Task ExecuteAsync(CancellationToken stopToken)
		{
			while (!stopToken.IsCancellationRequested)
			{
				if (await twitterUnfollowUser.GetStateAsync())
                {
					await Task.Run(() => twitterActions.UnFollowUser());
				}

				await Task.Delay(TimeSpan.FromMinutes(await twitterUnfollowUser.GenerateRandomAsync()), stopToken);
			}
		}
	}
}