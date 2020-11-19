using CryptoCore.Interfaces.Mongo.News;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoInfrastructure.Services
{
    internal class CryptoArticleService : BackgroundService
    {
        private readonly ICryptoArticleSettings cryptoArticles;

        public CryptoArticleService(ICryptoArticleSettings cryptoArticles)
        {
            this.cryptoArticles = cryptoArticles;
        }

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                if (await cryptoArticles.GetStateAsync())
                {
                    Task.WaitAll(cryptoArticles.Feed());
                }

                await Task.Delay(TimeSpan.FromMinutes(await cryptoArticles.GetFeedMinutesAsync()), stopToken);
            }
        }
    }
}