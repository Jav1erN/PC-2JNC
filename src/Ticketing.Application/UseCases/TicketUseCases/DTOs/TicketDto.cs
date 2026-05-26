namespace Ticketing.Application.TicketUseCases.DTOs;

public sealed record TicketDto(
    Guid TicketId,
    Guid UserId,
    string Username,
    string Title,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime? ClosedAt,
    IReadOnlyCollection<ResponseDto> Responses);
