using Ticketing.Domain.Enums;
using Ticketing.Domain.Exceptions;

namespace Ticketing.Domain.Entities;

public class Ticket
{
    private readonly List<Response> _responses = [];

    private Ticket()
    {
    }

    private Ticket(Guid ticketId, Guid userId, string title, string? description)
    {
        TicketId = ticketId;
        UserId = userId;
        Title = title;
        Description = description;
        Status = TicketStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid TicketId { get; private set; }

    public Guid UserId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public TicketStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ClosedAt { get; private set; }

    public User? User { get; private set; }

    public IReadOnlyCollection<Response> Responses => _responses.AsReadOnly();

    public static Ticket Create(Guid userId, string title, string? description)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Ticket title is required.", nameof(title));
        }

        return new Ticket(Guid.NewGuid(), userId, title.Trim(), string.IsNullOrWhiteSpace(description) ? null : description.Trim());
    }

    public void Close()
    {
        if (Status == TicketStatus.Closed)
        {
            throw new DomainException("Ticket is already closed.");
        }

        Status = TicketStatus.Closed;
        ClosedAt = DateTime.UtcNow;
    }
}
