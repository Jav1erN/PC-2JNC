using Ticketing.Application.DTOs.Roles;
using Ticketing.Application.Interfaces;

namespace Ticketing.Application.UseCases.Roles;

public sealed class AssignRoleToUserUseCase
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleToUserUseCase(IUserRepository users, IRoleRepository roles, IUnitOfWork unitOfWork)
    {
        _users = users;
        _roles = roles;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(AssignRoleRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User was not found.");

        var role = await _roles.GetByIdAsync(request.RoleId, cancellationToken)
            ?? throw new KeyNotFoundException("Role was not found.");

        user.AssignRole(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
