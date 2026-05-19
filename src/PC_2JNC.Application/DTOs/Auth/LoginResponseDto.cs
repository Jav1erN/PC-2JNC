namespace PC_2JNC.Application.DTOs.Auth;

public sealed record LoginResponseDto(
    string AccessToken,
    string TokenType,
    DateTime ExpiresAt);
