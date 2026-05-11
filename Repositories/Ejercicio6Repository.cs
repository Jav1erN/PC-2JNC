using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio6Repository
{
    private readonly LabDbContext _context;

    public Ejercicio6Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> Ejecutar(DateTime fecha)
    {
        return await _context.Orders
            .Where(o => o.OrderDate > fecha)
            .ToListAsync();
    }
}