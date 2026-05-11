using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio9Repository
{
    private readonly LabDbContext _context;

    public Ejercicio9Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<object> Ejecutar()
    {
        return await _context.Orders
            .GroupBy(o => o.ClientId)
            .Select(g => new { g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .FirstOrDefaultAsync();
    }
}