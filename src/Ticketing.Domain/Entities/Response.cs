namespace Ticketing.Domain.Entities;

public class Response
{
    private Response()
    {
    }

    private Response(Guid responseId, Guid ticketId, Guid responderId, string message)
    {
        ResponseId = responseId;
        TicketId = ticketId;
        ResponderId = responderId;
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid ResponseId { get; private set; }

    public Guid TicketId { get; private set; }

    public Guid ResponderId { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public Ticket? Ticket { get; private set; }

    public User? Responder { get; private set; }

    public static Response Create(Guid ticketId, Guid responderId, string message)
    {
        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException("Ticket id is required.", nameof(ticketId));
        }

        if (responderId == Guid.Empty)
        {
            throw new ArgumentException("Responder id is required.", nameof(responderId));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Response message is required.", nameof(message));
        }

        return new Response(Guid.NewGuid(), ticketId, responderId, message.Trim());
    }
}
