using Ticketing.Application.DTOs.Tickets;
using Ticketing.Application.Interfaces;
using Ticketing.Application.Services;

namespace Ticketing.Application.UseCases.Tickets;

public sealed class GetAllTicketsUseCase
{
    private readonly ITicketRepository _tickets;

    public GetAllTicketsUseCase(ITicketRepository tickets)
    {
        _tickets = tickets;
    }

    public async Task<IReadOnlyCollection<TicketDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var tickets = await _tickets.GetAllAsync(cancellationToken);
        return tickets.Select(TicketMapper.ToDto).ToArray();
    }
}
