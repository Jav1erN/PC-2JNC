using PC_2JNC.Application.DTOs.Auth;
using PC_2JNC.Application.Interfaces;

namespace PC_2JNC.Application.UseCases.Auth;

public sealed class AuthUseCase : IAuthUseCase
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthUseCase(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_jwtTokenService.GenerateToken(request));
    }
}
