using Ticketing.Application.DTOs.Tickets;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.UseCases.Tickets;

public sealed class CreateTicketUseCase
{
    private readonly ITicketRepository _tickets;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketUseCase(ITicketRepository tickets, IUnitOfWork unitOfWork)
    {
        _tickets = tickets;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(Guid userId, CreateTicketRequest request, CancellationToken cancellationToken = default)
    {
        var ticket = Ticket.Create(userId, request.Title, request.Description);
        await _tickets.AddAsync(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.TicketId;
    }
}
