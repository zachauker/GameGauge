using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeders;

public class UserSeed
{
    public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>
            {
                new AppUser { DisplayName = "Admin", UserName = "admin", Email = "admin@gamegauge.net" },
                new AppUser { DisplayName = "zach", UserName = "zwa5052", Email = "zach@gamegauge.net" },
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "PennState2019!");
            }
        }
    }
}