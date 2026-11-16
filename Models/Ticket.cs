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
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; } 
    public UserEntity? User { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}