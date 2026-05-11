using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio10Repository
{
    private readonly LabDbContext _context;

    public Ejercicio10Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<object> Ejecutar()
    {
        return await _context.OrderDetails
            .Select(d => new { d.OrderId, d.Product.Name, d.Quantity })
            .ToListAsync();
    }
}