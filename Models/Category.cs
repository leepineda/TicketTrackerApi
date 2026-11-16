namespace TicketTrackerApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Ticket> Tickets { get; set; } = new();
}