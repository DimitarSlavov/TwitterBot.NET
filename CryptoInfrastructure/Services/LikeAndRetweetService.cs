using CryptoCore.Interfaces.Twitter;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoInfrastructure.Services
{
    internal class LikeAndRetweetService : BackgroundService
    {
        private readonly ILikeAndRetweet likeAndRetweet;
        private readonly ILikeAndRetweetSettings likeAndRetweetSettings;

        public LikeAndRetweetService(
            ILikeAndRetweet likeAndRetweet,
            ILikeAndRetweetSettings likeAndRetweetSettings)
        {
            this.likeAndRetweet = likeAndRetweet;
            this.likeAndRetweetSettings = likeAndRetweetSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (await likeAndRetweetSettings.GetStateAsync())
                {
                    await likeAndRetweet.LikeAndRetweetWhitelistedAccount();
                }

                await Task.Delay(TimeSpan.FromMinutes(await likeAndRetweetSettings.GenerateRandomAsync()), stoppingToken);
            }
        }
    }
}
