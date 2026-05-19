using PC_2JNC.Application.DTOs.Auth;

namespace PC_2JNC.Application.UseCases.Auth;

public interface IAuthUseCase
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
