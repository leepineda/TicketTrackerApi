using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Data;
using TicketTrackerApi.DTOs;
using TicketTrackerApi.Models;

namespace TicketTrackerApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin/users")]
[ApiController]
public class AdminUsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminUsersController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromQuery] AccountStatus status = AccountStatus.Pending)
    {
        var users = await _context.AppUsers
            .Where(u => u.Status == status && u.Role != UserRole.Admin)
            .OrderBy(u => u.Id)
            .Select(u => new UserSummaryDto(
                u.Id,
                u.Username,
                u.Email,
                u.Department != null ? u.Department.Name : null,
                u.Role.ToString(),
                u.Status.ToString()))
            .ToListAsync();

        return Ok(users);
    }

    [HttpPost("{id:int}/approve")]
    public Task<IActionResult> Approve(int id) => ChangeStatus(id, AccountStatus.Approved);

    [HttpPost("{id:int}/reject")]
    public Task<IActionResult> Reject(int id) => ChangeStatus(id, AccountStatus.Rejected);

    private async Task<IActionResult> ChangeStatus(int id, AccountStatus newStatus)
    {
        var user = await _context.AppUsers.FindAsync(id);

        if (user is null)
            return NotFound(new { message = "User not found." });

        if (user.Role == UserRole.Admin)
            return StatusCode(403, new { message = "Admin accounts cannot be changed here." });

        if (user.Status != AccountStatus.Pending)
            return Conflict(new { message = $"User is already {user.Status}." });

        user.Status = newStatus;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{user.Username} is now {newStatus}." });
    }
}