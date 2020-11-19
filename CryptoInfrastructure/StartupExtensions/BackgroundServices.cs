using CryptoCore.Constants;
using CryptoInfrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoInfrastructure.StartupExtensions
{
	public static class BackgroundServices
	{
		public static void AddBackgroundServices(this IServiceCollection services)
		{
			if (!EnvironmentConstants.CurrentEnvironment.Equals(nameof(EnvironmentConstants.Development)))
			{
				services.AddHostedService<TwitterFollowUserService>();
				services.AddHostedService<TwitterUnFollowUserService>();
				services.AddHostedService<WakeService>();
				services.AddHostedService<TwitterService>();
				services.AddHostedService<LikeAndRetweetService>();
			}
		}
	}
}
