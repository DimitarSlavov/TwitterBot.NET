using Microsoft.Extensions.DependencyInjection;

namespace Twitter
{
	public static class TwitterServices
	{
		public static void AddTwitterServices(this IServiceCollection services)
		{
			services.AddSingleton<ITwitterActions, TwitterActions>();
		}
	}
}
