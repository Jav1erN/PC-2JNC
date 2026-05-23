namespace Ticketing.Application.DTOs.Tickets;

public sealed record CreateTicketRequest(string Title, string? Description);
