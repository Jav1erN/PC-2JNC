using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Application.TicketUseCases.DTOs;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.TicketUseCases.Queries;

public sealed record GetAllTicketsQuery : IRequest<IReadOnlyCollection<TicketDto>>;

internal sealed class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IReadOnlyCollection<TicketDto>>
{
    private readonly ITicketRepository _tickets;

    public GetAllTicketsQueryHandler(ITicketRepository tickets)
    {
        _tickets = tickets;
    }

    public async Task<IReadOnlyCollection<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _tickets.GetAllAsync(cancellationToken);
        return tickets.Select(ToDto).ToArray();
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
