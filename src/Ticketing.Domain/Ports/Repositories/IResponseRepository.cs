using Ticketing.Domain.Entities;

namespace Ticketing.Application.Interfaces;

public interface IResponseRepository
{
    Task AddAsync(Response response, CancellationToken cancellationToken = default);
}
