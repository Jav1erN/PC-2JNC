using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Application.UserUseCases.DTOs;

namespace Ticketing.Application.UserUseCases.Commands;

public sealed record LoginUserCommand(string Username, string Password) : IRequest<AuthResponse>;

internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository users,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var username = request.Username.Trim();
        var user = await _users.GetByUsernameAsync(username, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var roles = user.UserRoles
            .Where(userRole => userRole.Role is not null)
            .Select(userRole => userRole.Role!.RoleName)
            .ToArray();

        var token = _jwtTokenGenerator.GenerateToken(user, roles);
        return new AuthResponse(user.UserId, user.Username, token, roles);
    }
}
