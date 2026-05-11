using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio3Repository
{
    private readonly LabDbContext _context;

    public Ejercicio3Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<object> Ejecutar()
    {
        return await _context.OrderDetails
            .Where(d => d.OrderId == 1)
            .Select(d => new { d.Product.Name, d.Quantity })
            .ToListAsync();
    }
}