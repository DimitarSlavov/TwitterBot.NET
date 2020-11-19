using CryptoCore.Interfaces.Mongo.News;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoInfrastructure.Services
{
	internal class WakeService : BackgroundService
	{
		private readonly IWake wake;

		public WakeService(IWake wake)
		{
			this.wake = wake;
		}

		protected override async Task ExecuteAsync(CancellationToken stopToken)
		{
			while (!stopToken.IsCancellationRequested)
			{
				if (await wake.GetStateAsync())
                {
					await wake.WakeMe();
				}

				await Task.Delay(TimeSpan.FromMinutes(await wake.GenerateRandomAsync()), stopToken);
			}
		}
	}
}
