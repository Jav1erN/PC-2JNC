using MediatR;
using Ticketing.Application.Interfaces;

namespace Ticketing.Application.RoleUseCases.Commands;

public sealed record AssignRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<Unit>;

internal sealed class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, Unit>
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleToUserCommandHandler(IUserRepository users, IRoleRepository roles, IUnitOfWork unitOfWork)
    {
        _users = users;
        _roles = roles;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty)
        {
            throw new ArgumentException("User id is required.", nameof(request.UserId));
        }

        if (request.RoleId == Guid.Empty)
        {
            throw new ArgumentException("Role id is required.", nameof(request.RoleId));
        }

        var user = await _users.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User was not found.");

        var role = await _roles.GetByIdAsync(request.RoleId, cancellationToken)
            ?? throw new KeyNotFoundException("Role was not found.");

        user.AssignRole(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
