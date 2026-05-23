using Ticketing.Domain.Entities;

namespace Ticketing.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default);

    Task<Role?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Role>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Role role, CancellationToken cancellationToken = default);
}
