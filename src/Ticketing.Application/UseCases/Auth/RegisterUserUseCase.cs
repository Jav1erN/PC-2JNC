using Ticketing.Application.DTOs.Auth;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.UseCases.Auth;

public sealed class RegisterUserUseCase
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
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

    public async Task<AuthResponse> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _users.GetByUsernameAsync(request.Username, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Username is already registered.");
        }

        var user = User.Register(request.Username, _passwordHasher.Hash(request.Password), request.Email);
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
