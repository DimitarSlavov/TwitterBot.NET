using CryptoCore.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDbContext.Services;

namespace CryptoInfrastructure.StartupExtensions
{
    public static class MongoOptions
    {
        private static string ConcatPath(params string[] values)
        {
            return string.Join(":", values);
        }

        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(op =>
            {
                op.ConnectionString = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.ConnectionString))).Value;

                op.News = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Database), nameof(op.News))).Value;

                op.Accounts = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Database), nameof(op.Accounts))).Value;

                op.Twitter = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Database), nameof(op.Twitter))).Value;

                op.CryptoArticleSettings = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Database), nameof(op.CryptoArticleSettings))).Value;

                op.CryptoArticle = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Table), nameof(op.CryptoArticle))).Value;

                op.Wake = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Table), nameof(op.Wake))).Value;

                op.TwitterStatus = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Table), nameof(op.TwitterStatus))).Value;

                op.TwitterAccounts = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Table), nameof(op.TwitterAccounts))).Value;

                op.YouTubeAccounts = configuration.GetSection(
                    ConcatPath(nameof(op.MongoConnection), nameof(op.Table), nameof(op.YouTubeAccounts))).Value;
            });

            var mongoConnection = configuration.GetSection(
                ConcatPath(nameof(MongoSettings.MongoConnection), nameof(MongoSettings.ConnectionString))).Value;

            var newsDb = configuration.GetSection(
                ConcatPath(nameof(MongoSettings.MongoConnection), nameof(MongoSettings.Database), nameof(MongoSettings.News))).Value;

            var accountsDb = configuration.GetSection(
                ConcatPath(nameof(MongoSettings.MongoConnection), nameof(MongoSettings.Database), nameof(MongoSettings.Accounts))).Value;

            var twitterDb = configuration.GetSection(
                ConcatPath(nameof(MongoSettings.MongoConnection), nameof(MongoSettings.Database), nameof(MongoSettings.Twitter))).Value;

            services.AddTransient(s => new CryptoArticleRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.CryptoArticle)}").Value
                }
            ));

            services.AddTransient(s => new CryptoArticleSettingsRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.CryptoArticleSettings)}").Value
                }
            ));

            services.AddTransient(s => new WakeRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.Wake)}").Value
                }
            ));

            services.AddTransient(s => new TwitterStatusRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.TwitterStatus)}").Value
                }
            ));

            services.AddTransient(s => new TwitterFollowUserRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.TwitterFollow)}").Value
                }
            ));

            services.AddTransient(s => new TwitterUnfollowUserRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = newsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.TwitterUnfollow)}").Value
                }
            ));

            services.AddTransient(s => new TwitterAccountsRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = accountsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.TwitterAccounts)}").Value
                }
            ));

            services.AddTransient(s => new YouTubeAccountsRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = accountsDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.YouTubeAccounts)}").Value
                }
            ));

            services.AddTransient(s => new LikeAndRetweetSettingsRepository(
                new DbSettings
                {
                    ConnectionString = mongoConnection,
                    Database = twitterDb,
                    Table = configuration.GetSection($"{nameof(MongoSettings.MongoConnection)}:{nameof(MongoSettings.Table)}:{nameof(MongoSettings.LikeAndRetweetSettings)}").Value
                }
            ));
        }
    }
}
