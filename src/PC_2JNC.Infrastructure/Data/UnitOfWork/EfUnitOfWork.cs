using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;

namespace PC_2JNC.Infraestructura.Data.UnitOfWork;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly LabDbContext _context;

    public EfUnitOfWork(LabDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
