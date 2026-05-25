namespace Ticketing.Application.TicketUseCases.DTOs;

public sealed record ResponseDto(
    Guid ResponseId,
    Guid TicketId,
    Guid ResponderId,
    string ResponderUsername,
    string Message,
    DateTime CreatedAt);
