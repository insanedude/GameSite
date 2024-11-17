using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GameSiteProject.Models;

public class GameSiteSeedContext
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        const string userEmail = "user@gmail.com";
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            var defaultUser = new User() { Email = userEmail, UserName = userEmail, Nickname = "User" };
            await userManager.CreateAsync(defaultUser, "password");
        }

        const string adminEmail = "admin@gmail.com";
        user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            var defaultUser = new User() { Email = adminEmail, UserName = adminEmail, Nickname = "Admin" };
            await userManager.CreateAsync(defaultUser, "password");
            user = defaultUser;
        }
        if (!await roleManager.RoleExistsAsync(GameSiteConstants.AdminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(GameSiteConstants.AdminRole));
        }
        if (!await userManager.IsInRoleAsync(user, GameSiteConstants.AdminRole))
        {
            await userManager.AddToRoleAsync(user, GameSiteConstants.AdminRole);
        }
    }
}