    using System.ComponentModel.DataAnnotations;
    using TicketTrackerApi.Validators;

    namespace TicketTrackerApi.DTOs;
    public class RegistrationDto
    {
        public string Username { get; set; } = "";
        
        [EmailAddress]
        public string Email { get; set; } = "";
    
        [Required]
        [DataType(DataType.Password)] 
        [ValidatePassword]          
        public string PasswordHash { get; set; } = "";
    }