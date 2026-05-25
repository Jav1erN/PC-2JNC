using MediatR;
using Ticketing.Application.Interfaces;

namespace Ticketing.Application.TicketUseCases.Commands;

public sealed record CloseTicketCommand(Guid TicketId) : IRequest<Unit>;

internal sealed class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand, Unit>
{
    private readonly ITicketRepository _tickets;
    private readonly IUnitOfWork _unitOfWork;

    public CloseTicketCommandHandler(ITicketRepository tickets, IUnitOfWork unitOfWork)
    {
        _tickets = tickets;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CloseTicketCommand request, CancellationToken cancellationToken)
    {
        if (request.TicketId == Guid.Empty)
        {
            throw new ArgumentException("Ticket id is required.", nameof(request.TicketId));
        }

        var ticket = await _tickets.GetByIdAsync(request.TicketId, cancellationToken)
            ?? throw new KeyNotFoundException("Ticket was not found.");

        ticket.Close();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
