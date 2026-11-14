
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Data;
using TicketTrackerApi.Models;

namespace TicketTrackerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        var tickets = await _context.Tickets
            .FromSqlRaw("SELECT * FROM Tickets")
            .ToListAsync();

        return tickets;
    }

    // GET: api/tickets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        var ticket = await _context.Tickets
            .FromSqlInterpolated( // interpolation helps since we pass a value and interpolation makes it prevent sql injection
                $"SELECT * FROM Tickets WHERE Id = {id}")
            .FirstOrDefaultAsync();

        if (ticket == null)
        {
            return NotFound();
        }

        return ticket;
    }

    // POST: api/tickets
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTicket),
            new { id = ticket.Id },
            ticket);
    }

    // DELETE: api/tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}
