namespace TicketTrackerApi.Models;

public enum UserRole
{
    Member = 0,
    Technician = 1,
    Admin = 2,
}

public enum AccountStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}

public class AppUser
{
    public int Id { get; set; } 
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = ""; // Secure hash on the Controller = AuthController.cs
    public UserRole Role { get; set; } = UserRole.Member;
    public AccountStatus Status { get; set; } = AccountStatus.Pending;

    //One user can have many tickets
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public int? DepartmentId { get; set; }      // the foreign key column
    public Department? Department { get; set; } // navigation property
}