using Ticketing.Application.DTOs.Tickets;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.Services;

public static class TicketMapper
{
    public static TicketDto ToDto(Ticket ticket)
    {
        var responses = ticket.Responses
            .OrderBy(response => response.CreatedAt)
            .Select(response => new ResponseDto(
                response.ResponseId,
                response.TicketId,
                response.ResponderId,
                response.Responder?.Username ?? string.Empty,
                response.Message,
                response.CreatedAt))
            .ToArray();

        return new TicketDto(
            ticket.TicketId,
            ticket.UserId,
            ticket.User?.Username ?? string.Empty,
            ticket.Title,
            ticket.Description,
            ticket.Status.ToString(),
            ticket.CreatedAt,
            ticket.ClosedAt,
            responses);
    }
}
