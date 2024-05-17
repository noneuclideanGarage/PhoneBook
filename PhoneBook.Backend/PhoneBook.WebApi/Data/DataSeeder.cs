using Microsoft.AspNetCore.Identity;
using PhoneBook.WebApi.Models;
using Serilog;

namespace PhoneBook.WebApi.Data;

public class DataSeeder
{
    /// <summary>
    ///     Seed users and roles in the Identity database.
    /// </summary>
    /// <param name="userManager">ASP.NET Core Identity User Manager</param>
    /// <param name="roleManager">ASP.NET Core Identity Role Manager</param>
    /// <returns></returns>
    public static async Task SeedAsync(UserManager<AppUser> userManager)
    {
        Log.Information("Start seeding...");
        // New admin user
        var newAdminUser = new AppUser
        {
            UserName = "test"
        };

        var createdUser = await userManager.CreateAsync(newAdminUser, "Test@1234567");
        
        Log.Information("created admin user: {@createdUser}", createdUser);

        var roleResult = await userManager.AddToRoleAsync(newAdminUser, "Admin");
        
        Log.Information("role result: {@roleResult}", roleResult);
        Log.Information("End seeding.");
    }
}