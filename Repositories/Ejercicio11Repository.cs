using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio11Repository
{
    private readonly LabDbContext _context;

    public Ejercicio11Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Ejecutar()
    {
        return await _context.OrderDetails
            .Where(d => d.Order.ClientId == 1)
            .Select(d => d.Product.Name)
            .Distinct()
            .ToListAsync();
    }
}