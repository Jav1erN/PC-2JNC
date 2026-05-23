using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Persistence;

namespace Ticketing.Infrastructure.Repositories;

public sealed class ResponseRepository : IResponseRepository
{
    private readonly AppDbContext _dbContext;

    public ResponseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(Response response, CancellationToken cancellationToken = default)
    {
        return _dbContext.Responses.AddAsync(response, cancellationToken).AsTask();
    }
}
