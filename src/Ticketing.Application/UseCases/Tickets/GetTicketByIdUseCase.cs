using Ticketing.Application.DTOs.Tickets;
using Ticketing.Application.Interfaces;
using Ticketing.Application.Services;

namespace Ticketing.Application.UseCases.Tickets;

public sealed class GetTicketByIdUseCase
{
    private readonly ITicketRepository _tickets;

    public GetTicketByIdUseCase(ITicketRepository tickets)
    {
        _tickets = tickets;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        return TicketMapper.ToDto(ticket);
    }
}
