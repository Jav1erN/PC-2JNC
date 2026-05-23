using Ticketing.Application.DTOs.Tickets;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;

namespace Ticketing.Application.UseCases.Tickets;

public sealed class AddResponseToTicketUseCase
{
    private readonly ITicketRepository _tickets;
    private readonly IResponseRepository _responses;
    private readonly IUnitOfWork _unitOfWork;

    public AddResponseToTicketUseCase(ITicketRepository tickets, IResponseRepository responses, IUnitOfWork unitOfWork)
    {
        _tickets = tickets;
        _responses = responses;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(Guid ticketId, Guid responderId, AddResponseRequest request, CancellationToken cancellationToken = default)
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new InvalidOperationException("Closed tickets cannot receive responses.");
        }

        var response = Response.Create(ticketId, responderId, request.Message);
        await _responses.AddAsync(response, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return response.ResponseId;
    }
}
