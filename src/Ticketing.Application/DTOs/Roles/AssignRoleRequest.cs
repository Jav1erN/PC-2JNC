namespace Ticketing.Application.DTOs.Roles;

public sealed record AssignRoleRequest(Guid UserId, Guid RoleId);
