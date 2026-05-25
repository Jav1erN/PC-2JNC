using Microsoft.EntityFrameworkCore;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Persistence;

namespace Ticketing.Infrastructure.Repositories;

public sealed class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _dbContext;

    public TicketRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Ticket?> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken = default, bool asNoTracking = false)
    {
        IQueryable<Ticket> query = _dbContext.Tickets
            .Include(ticket => ticket.User)
            .Include(ticket => ticket.Responses)
            .ThenInclude(response => response.Responder);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.FirstOrDefaultAsync(ticket => ticket.TicketId == ticketId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Tickets
            .AsNoTracking()
            .Include(ticket => ticket.User)
            .Include(ticket => ticket.Responses)
            .ThenInclude(response => response.Responder)
            .OrderByDescending(ticket => ticket.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default)
    {
        return _dbContext.Tickets.AddAsync(ticket, cancellationToken).AsTask();
    }
}
