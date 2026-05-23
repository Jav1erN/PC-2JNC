namespace Ticketing.Application.DTOs.Auth;

public sealed record RegisterUserRequest(string Username, string Password, string? Email);
