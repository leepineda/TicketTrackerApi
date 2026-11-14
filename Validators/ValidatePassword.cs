using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TicketTrackerApi.Validators;

public class ValidatePasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string password)
        {
            // 1. Check for at least one uppercase letter (A-Z)
            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");

            // 2. Check for at least one lowercase letter (a-z)
            bool hasLower = Regex.IsMatch(password, @"[a-z]");

            // 3. Check for at least one digit (0-9)
            bool hasDigit = Regex.IsMatch(password, @"\d");

            // 4. Check for at least one special character (symbols like @, $, !, %, *, ?, &, or underscores)
            bool hasSpecial = Regex.IsMatch(password, @"[\W_]");

            // Combine the rules (e.g., minimum 8 characters + all requirements)
            if (password.Length < 8 || !hasUpper || !hasLower || !hasDigit || !hasSpecial)
            {
                return new ValidationResult(ErrorMessage ?? "Password must be at least 8 characters long and include an uppercase letter, lowercase letter, number, and special character.");
            }
        }

        return ValidationResult.Success;
    }
}