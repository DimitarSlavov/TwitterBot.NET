using CryptoCore.Interfaces.Mongo.Accounts;
using CryptoCore.Interfaces.Mongo.News;
using CryptoCore.Interfaces.Twitter;
using CryptoInfrastructure.MongoDbContext.Accounts;
using CryptoInfrastructure.MongoDbContext.News;
using CryptoInfrastructure.TwitterContext;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoInfrastructure.StartupExtensions
{
	public static class Services
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddSingleton<ICryptoArticleSettings, CryptoArticleSettings>();
			services.AddSingleton<ITwitterStatus, TwitterStatus>();
			services.AddSingleton<IWake, Wake>();
			services.AddSingleton<ILikeAndRetweet, LikeAndRetweet>();
			services.AddTransient<ICryptoArticle, CryptoArticle>();
			services.AddTransient<ITwitterFollowUser, TwitterFollowUser>();
			services.AddTransient<ITwitterUnfollowUser, TwitterUnfollowUser>();
			services.AddTransient<ITwitterAccounts, TwitterAccounts>();
			services.AddTransient<IYouTubeAccounts, YouTubeAccounts>();
			services.AddTransient<ILikeAndRetweetSettings, LikeAndRetweetSettings>();
		}
	}
}
