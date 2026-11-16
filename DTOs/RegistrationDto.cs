    using System.ComponentModel.DataAnnotations;
    using TicketTrackerApi.Validators;

    namespace TicketTrackerApi.DTOs;
    public class RegistrationDto
    {
        [Required]
        public string Username { get; set; } = "";
        
        [Required, EmailAddress]
        public string Email { get; set; } = "";
    
        [Required]
        [DataType(DataType.Password)] 
        [ValidatePassword]          
        public string Password { get; set; } = "";

        public int? DepartmentId { get; set; }
    }