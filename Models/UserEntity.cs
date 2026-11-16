namespace TicketTrackerApi.Models;

public class UserEntity
{
    public int Id { get; set; } 
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = ""; // Secure hash on the Controller = AuthController.cs

    //One user can have many tickets
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public int? DepartmentId { get; set; }      // the foreign key column
    public Department? Department { get; set; } // navigation property
}