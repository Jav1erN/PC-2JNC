using PC_2JNC.Application.DTOs.Auth;

namespace PC_2JNC.Application.Interfaces;

public interface IJwtTokenService
{
    LoginResponseDto? GenerateToken(LoginRequestDto request);
}
