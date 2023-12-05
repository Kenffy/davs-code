using Authenticate.Models;
using Authenticate.Utility;
using Microsoft.AspNetCore.Identity;

namespace Authenticate.Data
{
    public class AppIdentitySeedDbContext
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var adminUser = new AppUser
                {
                    DisplayName = "Kenffy",
                    Email = "kenffy@gmail.com",
                    UserName = "kenffy@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Kenffy",
                        LastName = "Dave",
                        Street = "Kenffy Street 19",
                        City = "Stuttgart",
                        State = "Germany",
                        ZipCode = "71332"
                    }
                };

                var cusUser = new AppUser
                {
                    DisplayName = "Parfait",
                    Email = "Parfait@gmail.com",
                    UserName = "Parfait@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Parfait",
                        LastName = "Dupleix",
                        Street = "Parfait Street 65",
                        City = "Essen",
                        State = "Germany",
                        ZipCode = "50234"
                    }
                };

                await userManager.CreateAsync(adminUser, "Kenffy420*");
                await userManager.CreateAsync(cusUser, "Parfait420*");

                if (!roleManager.RoleExistsAsync(Constant.AdminRole).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    roleManager.CreateAsync(new IdentityRole(Constant.AdminRole)).GetAwaiter().GetResult();
                }

                if (!roleManager.RoleExistsAsync(Constant.CustomerRole).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    roleManager.CreateAsync(new IdentityRole(Constant.CustomerRole)).GetAwaiter().GetResult();
                }

                await userManager.AddToRoleAsync(adminUser, Constant.AdminRole);
                await userManager.AddToRoleAsync(cusUser, Constant.CustomerRole);
            }
        }
    }
}
