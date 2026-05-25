using MediatR;
using Ticketing.Application.Interfaces;
using Ticketing.Application.RoleUseCases.DTOs;

namespace Ticketing.Application.RoleUseCases.Queries;

public sealed record GetAllRolesQuery : IRequest<IReadOnlyCollection<RoleDto>>;

internal sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IReadOnlyCollection<RoleDto>>
{
    private readonly IRoleRepository _roles;

    public GetAllRolesQueryHandler(IRoleRepository roles)
    {
        _roles = roles;
    }

    public async Task<IReadOnlyCollection<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roles.GetAllAsync(cancellationToken);
        return roles.Select(role => new RoleDto(role.RoleId, role.RoleName)).ToArray();
    }
}
