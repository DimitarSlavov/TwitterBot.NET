using CryptoCore.Interfaces.Mongo.News;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Twitter;

namespace CryptoInfrastructure.Services
{
	internal class TwitterFollowUserService : BackgroundService
	{
		private readonly ITwitterFollowUser twitterFollowUser;
		private readonly ITwitterActions twitterActions;

		public TwitterFollowUserService(
			ITwitterFollowUser twitterFollowUser,
			ITwitterActions twitterActions)
		{
			this.twitterFollowUser = twitterFollowUser;
			this.twitterActions = twitterActions;
		}

		protected override async Task ExecuteAsync(CancellationToken stopToken)
		{
			while (!stopToken.IsCancellationRequested)
			{
				if (await twitterFollowUser.GetStateAsync())
				{
					await Task.Run(() => twitterActions.FollowUser());
				}

				await Task.Delay(TimeSpan.FromMinutes(await twitterFollowUser.GenerateRandomAsync()), stopToken);
			}
		}
	}
}