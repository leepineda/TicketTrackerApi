using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Models;

namespace TicketTrackerApi.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<AppUser>>();

        if (await db.AppUsers.AnyAsync(u => u.Role == UserRole.Admin))
            return; // an admin already exists

        var email = config["Admin:Email"];
        var username = config["Admin:Username"];
        var password = config["Admin:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username)
            || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("AdminSeeder: Admin__* values missing in .env, skipping.");
            return;
        }

        var admin = new AppUser
        {
            Username = username.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            Role = UserRole.Admin,
            Status = AccountStatus.Approved
        };
        admin.Password = hasher.HashPassword(admin, password);

        db.AppUsers.Add(admin);
        await db.SaveChangesAsync();
    }
}