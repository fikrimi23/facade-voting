using App.Enum;
using Microsoft.AspNetCore.Identity;

namespace app.Data
{
    public class IdentityDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("gamer.fikri@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
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