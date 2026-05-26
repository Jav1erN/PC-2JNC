using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Application.TicketUseCases.DTOs;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.TicketUseCases.Queries;

public sealed record GetTicketByIdQuery(Guid TicketId) : IRequest<TicketDto>;

internal sealed class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly ITicketRepository _tickets;

    public GetTicketByIdQueryHandler(ITicketRepository tickets)
    {
        _tickets = tickets;
    }

    public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.TicketId == Guid.Empty)
        {
            throw new ArgumentException("Ticket id is required.", nameof(request.TicketId));
        }

        var ticket = await _tickets.GetByIdAsync(request.TicketId, cancellationToken, asNoTracking: true)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        return ToDto(ticket);
    }

    private static TicketDto ToDto(Ticket ticket)
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
