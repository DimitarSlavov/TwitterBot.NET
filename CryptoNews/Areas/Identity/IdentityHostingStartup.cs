using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CryptoNews.Areas.Identity.IdentityHostingStartup))]
namespace CryptoNews.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
			});
		}
	}
}