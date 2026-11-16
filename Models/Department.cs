namespace TicketTrackerApi.Models;
    public class Department 
 {
    public int Id { get; set; }
    public string Name { get; set; } = "";

     public List<UserEntity> Users { get; set; } = new();
}
