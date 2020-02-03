using Microsoft.AspNetCore.Identity;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin
{
    public class IdentityDataInitializer
    {
        public static void SeedRoles
            (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("Mod").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Mod"
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
                ("Admin=").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }

        public static void SeedUsers
            (UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync
                    ("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@ninjasaigon.com"
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "PDC@@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Admin").Wait();
                }
            }


            if (userManager.FindByNameAsync
                    ("user2").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "mod",
                    Email = "mod@ninjasaigon.com"
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "PDC@@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Mod").Wait();
                }
            }
        }

        public static void SeedData
        (UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    }
}
