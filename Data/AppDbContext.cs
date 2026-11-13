using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Models;

namespace TicketTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
}