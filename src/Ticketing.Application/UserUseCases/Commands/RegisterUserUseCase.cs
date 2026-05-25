using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Application.UserUseCases.DTOs;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.UserUseCases.Commands;

public sealed record RegisterUserCommand(string Username, string Password, string? Email) : IRequest<AuthResponse>;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository users,
        IRoleRepository roles,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _users = users;
        _roles = roles;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            throw new ArgumentException("Username is required.", nameof(request.Username));
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Password is required.", nameof(request.Password));
        }

        var username = request.Username.Trim();
        var existingUser = await _users.GetByUsernameAsync(username, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Username is already registered.");
        }

        var user = User.Register(username, _passwordHasher.Hash(request.Password), request.Email);
        var clientRole = await _roles.GetByNameAsync("Client", cancellationToken);
        var roleNames = new List<string>();

        if (clientRole is not null)
        {
            user.AssignRole(clientRole);
            roleNames.Add(clientRole.RoleName);
        }

        await _users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user, roleNames);
        return new AuthResponse(user.UserId, user.Username, token, roleNames);
    }
}
