using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Data;
using TicketTrackerApi.DTOs;
using TicketTrackerApi.Models;
using TicketTrackerApi.Services;

namespace TicketTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthController(
        AppDbContext context,
        IPasswordHasher<AppUser> passwordHasher,
        ITokenService tokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var username = dto.Username.Trim();

        if (await _context.AppUsers.AnyAsync(u => u.Email == email))
            return Conflict(new { message = "Email is already registered." });

        if (await _context.AppUsers.AnyAsync(u => u.Username == username))
            return Conflict(new { message = "Username is already taken." });

        if (!await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId))
            return BadRequest(new { message = "Department does not exist." });

        var user = new AppUser
        {
            Username = username,
            Email = email,
            DepartmentId = dto.DepartmentId
        };

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        _context.AppUsers.Add(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // TODO(sec): check the SQL error number (2601/2627) instead of catching every update error
            return Conflict(new { message = "Email or username is already taken." });
        }

        return Ok(new { message = "Registered. Your account is waiting for admin approval." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);

        if (user is null)
            return Unauthorized(new { message = "Invalid email or password." });

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized(new { message = "Invalid email or password." });

        if (user.Status == AccountStatus.Pending)
            return StatusCode(403, new { message = "Your account is waiting for admin approval." });

        if (user.Status == AccountStatus.Rejected)
            return StatusCode(403, new { message = "Your account was not approved." });

        // TODO(sec): rate limit login attempts
        // TODO(sec): dummy hash when the email isn't found (timing difference)
        var token = _tokenService.CreateToken(user);

        return Ok(new
        {
            token,
            user = new { user.Id, user.Username, user.Email, Role = user.Role.ToString() }
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me() => Ok(new
    {
        Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
        Role = User.FindFirstValue(ClaimTypes.Role)
    });
}