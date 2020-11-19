using CryptoCore.Constants;
using CryptoInfrastructure.StartupExtensions;
using CryptoNews.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter;

namespace CryptoNews
{
	public class Startup
	{
        [System.Obsolete]
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }

        [System.Obsolete]
        private IHostingEnvironment Environment { get; }

        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
		{
			AuthorizationConstants.SetAdminUsername(Configuration["Admin:Email"]);
			MongoConstants.SetMongoConnect(Configuration["Mongo:Connect"]);

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("ElephantConnection")));

			services.AddIdentity<IdentityUser, IdentityRole<long>>()
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddRazorPages();

			EnvironmentConstants.SetCurrentEnvironment(Environment.EnvironmentName);

			//Add new custom services here
			services.AddServices();
			services.AddTwitterServices();
			services.AddBackgroundServices();
			services.AddMongoDb(Configuration);
			//
		}

        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(e =>
			{
				e.MapRazorPages();
			});
		}
	}
}
