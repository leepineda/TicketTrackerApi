    using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TicketTrackerApi.Validators;

    namespace TicketTrackerApi.DTOs;
    public class RegistrationDto
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = "";
        
        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; } = "";
    
        [Required, StringLength(128, MinimumLength = 8)]
        [DataType(DataType.Password)] 
        [ValidatePassword]          
        public string Password { get; set; } = "";

        [Required]
        public int? DepartmentId { get; set; }
    }