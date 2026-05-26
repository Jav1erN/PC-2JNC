namespace Ticketing.Application.UserUseCases.DTOs;

public sealed record AuthResponse(Guid UserId, string Username, string Token, IReadOnlyCollection<string> Roles);
