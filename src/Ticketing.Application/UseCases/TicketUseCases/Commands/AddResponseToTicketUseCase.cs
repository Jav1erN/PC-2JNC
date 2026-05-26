using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;

namespace Ticketing.Application.TicketUseCases.Commands;

public sealed record AddResponseRequest(string Message);

public sealed record AddResponseToTicketCommand(Guid TicketId, Guid ResponderId, string Message) : IRequest<Guid>;

internal sealed class AddResponseToTicketCommandHandler : IRequestHandler<AddResponseToTicketCommand, Guid>
{
    private readonly ITicketRepository _tickets;
    private readonly IResponseRepository _responses;
    private readonly IUnitOfWork _unitOfWork;

    public AddResponseToTicketCommandHandler(
        ITicketRepository tickets,
        IResponseRepository responses,
        IUnitOfWork unitOfWork)
    {
        _tickets = tickets;
        _responses = responses;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddResponseToTicketCommand request, CancellationToken cancellationToken)
    {
        if (request.TicketId == Guid.Empty)
        {
            throw new ArgumentException("Ticket id is required.", nameof(request.TicketId));
        }

        if (request.ResponderId == Guid.Empty)
        {
            throw new ArgumentException("Responder id is required.", nameof(request.ResponderId));
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Response message is required.", nameof(request.Message));
        }

        var ticket = await _tickets.GetByIdAsync(request.TicketId, cancellationToken)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new InvalidOperationException("Closed tickets cannot receive responses.");
        }

        var response = Response.Create(request.TicketId, request.ResponderId, request.Message);
        await _responses.AddAsync(response, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response.ResponseId;
    }
}
