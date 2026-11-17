namespace TicketTrackerApi.DTOs;

public record UserSummaryDto(
    int Id,
    string Username,
    string Email,
    string? Department,
    string Role,
    string Status);