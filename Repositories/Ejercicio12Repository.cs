using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio12Repository
{
    private readonly LabDbContext _context;

    public Ejercicio12Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Ejecutar()
    {
        return await _context.OrderDetails
            .Where(d => d.ProductId == 2)
            .Select(d => d.Order.Client.Name)
            .Distinct()
            .ToListAsync();
    }
}