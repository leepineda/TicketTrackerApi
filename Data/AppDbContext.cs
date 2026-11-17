using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Models; // Get the models eg.
                               //Ticket.cs and User.cs

// This makes it possible to make the database inside c# INITIALIZES THEM
// But not only that its the bridge to connect to the SQl server or any server?

namespace TicketTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //This makes the TABLES 
    public DbSet<Ticket> Tickets=> Set<Ticket>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Hardware" },
            new Category { Id = 2, Name = "Software" },
            new Category { Id = 3, Name = "Network" },
            new Category { Id = 4, Name = "Account & Access" },
            new Category { Id = 5, Name = "Email" },
            new Category { Id = 6, Name = "Security" },
            new Category { Id = 7, Name = "Service Request" },
            new Category { Id = 8, Name = "Other" }
        );

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "College of Engineering" },
            new Department { Id = 2, Name = "College of Education" },
            new Department { Id = 3, Name = "College of Information Technology" },
            new Department { Id = 4, Name = "Registrar's Office" },
            new Department { Id = 5, Name = "Library" },
            new Department { Id = 6, Name = "Accounting Office" },
            new Department { Id = 7, Name = "OAR" },
            new Department { Id = 8, Name = "Other" }
            );

        modelBuilder.Entity<AppUser>(user =>
        {
            user.Property(u => u.Username).HasMaxLength(50);
            user.Property(u => u.Email).HasMaxLength(256);
            user.HasIndex(u => u.Username).IsUnique();
            user.HasIndex(u => u.Email).IsUnique();
        });
    }
}