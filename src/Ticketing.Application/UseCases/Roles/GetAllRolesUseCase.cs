using Ticketing.Application.DTOs.Roles;
using Ticketing.Application.Interfaces;

namespace Ticketing.Application.UseCases.Roles;

public sealed class GetAllRolesUseCase
{
    private readonly IRoleRepository _roles;

    public GetAllRolesUseCase(IRoleRepository roles)
    {
        _roles = roles;
    }

    public async Task<IReadOnlyCollection<RoleDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roles.GetAllAsync(cancellationToken);
        return roles.Select(role => new RoleDto(role.RoleId, role.RoleName)).ToArray();
    }
}
