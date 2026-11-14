using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Data;
using TicketTrackerApi.DTOs;
using TicketTrackerApi.Models;

namespace TicketTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    // ASP.NET Core's built-in password hasher
    private readonly PasswordHasher<UserEntity> _passwordHasher = new();

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto dto)
    {

        //  Check if the email is already taken in the database
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
        {
            return BadRequest(new { message = "Email is already registered." });
        }

        //  Map the DTO data to your permanent User database entity
        var user = new UserEntity
        {
            Username = dto.Username,
            Email = dto.Email
        };

        //  Securely hash the plain-text password from the DTO
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.PasswordHash);

        //  Add to Entity Framework Core tracking and save to MSSQL
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully!" });
    }
}