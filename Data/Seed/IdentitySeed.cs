using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Timesheet.Models;

namespace Timesheet.Data.Seed;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        string[] roles = ["Admin", "Client"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = configuration["SeedSettings:AdminEmail"] ?? "admin@gmtech.co.za";
        var adminPassword = configuration["SeedSettings:AdminPassword"];

        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException("Seed admin password is missing. Configure SeedSettings:AdminPassword.");
        }

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Administrator",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, adminPassword);
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var clientEmail = configuration["SeedSettings:ClientEmail"] ?? "client@mopani.co.za";
        var clientPassword = configuration["SeedSettings:ClientPassword"];

        if (string.IsNullOrWhiteSpace(clientPassword))
        {
            throw new InvalidOperationException("Seed client password is missing. Configure SeedSettings:ClientPassword.");
        }

        var mopaniClient = await dbContext.Clients
            .FirstOrDefaultAsync(client => client.Name.Contains("Mopani"));

        var clientUser = await userManager.FindByEmailAsync(clientEmail);

        if (clientUser is null)
        {
            clientUser = new ApplicationUser
            {
                UserName = clientEmail,
                Email = clientEmail,
                FullName = "Mopani Client User",
                ClientId = mopaniClient?.Id,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(clientUser, clientPassword);
        }
        else
        {
            clientUser.FullName = "Mopani Client User";
            clientUser.ClientId = mopaniClient?.Id;
            await userManager.UpdateAsync(clientUser);
        }

        if (!await userManager.IsInRoleAsync(clientUser, "Client"))
        {
            await userManager.AddToRoleAsync(clientUser, "Client");
        }
    }
}
