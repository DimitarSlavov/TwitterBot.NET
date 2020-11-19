using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CryptoCore.Constants;

namespace CryptoNews.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding custom roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = 
			{
				nameof(AuthorizationConstants.Admin),
				nameof(AuthorizationConstants.User)
			};

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            };

            //creating a super user who could maintain the web app
            var powerUser = new IdentityUser
            {
                UserName = Configuration["Admin:Email"],
                Email = Configuration["Admin:Email"]
            };

            var userPassword = "";
            var user = await userManager.FindByEmailAsync(Configuration["Admin:Email"]);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await userManager.AddToRoleAsync(powerUser, nameof(AuthorizationConstants.Admin));

                }
            }
        }
    }
}
