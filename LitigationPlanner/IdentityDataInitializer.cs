using LitigationPlanner.Data.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace LitigationPlanner.MVC
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@email.com";

                IdentityResult result = userManager.CreateAsync(user, "Password123$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole();
                role.Name = "Administrator";

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
