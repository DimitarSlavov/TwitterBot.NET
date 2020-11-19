using CryptoCore.Interfaces.Mongo.News;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoInfrastructure.Services
{
	internal class TwitterService : BackgroundService
	{
		private readonly ITwitterStatus twitterStatus;

		public TwitterService(ITwitterStatus twitterStatus)
		{
			this.twitterStatus = twitterStatus;
		}

		protected override async Task ExecuteAsync(CancellationToken stopToken)
		{
			while (!stopToken.IsCancellationRequested)
			{
				if (await twitterStatus.GetStateAsync())
                {
					await twitterStatus.TweetArticlesAsStatus();
				}

				await Task.Delay(TimeSpan.FromMinutes(await twitterStatus.GenerateRandomAsync()), stopToken);
			}
		}
	}
}
