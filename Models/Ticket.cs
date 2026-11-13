namespace TicketTrackerApi.Models;

public enum TicketStatus
{
    Open,
    InProgress,
    Closed
}

public enum PriorityLevel
{
    Low,
    Medium,
    High,
    Urgent
}

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

