using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Models; // Get the models eg.
                               //Ticket.cs and User.cs

// This makes it possible to make the database inside c# INITIALIZES THEM

namespace TicketTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //This makes the TABLES 
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
}