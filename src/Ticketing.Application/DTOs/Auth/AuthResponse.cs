namespace Ticketing.Application.DTOs.Auth;

public sealed record AuthResponse(Guid UserId, string Username, string Token, IReadOnlyCollection<string> Roles);
