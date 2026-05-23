using Ticketing.Application.Interfaces;

namespace Ticketing.Application.UseCases.Tickets;

public sealed class CloseTicketUseCase
{
    private readonly ITicketRepository _tickets;
    private readonly IUnitOfWork _unitOfWork;

    public CloseTicketUseCase(ITicketRepository tickets, IUnitOfWork unitOfWork)
    {
        _tickets = tickets;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        ticket.Close();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
