using App.Enum;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Data
{
    public class IdentityDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("gamer.fikri@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "gamer.fikri@gmail.com",
                    Email = "gamer.fikri@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, ":C1nt4:Fu-kun;").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Role.Admin).Wait();
                }
            }
        }
    }
}