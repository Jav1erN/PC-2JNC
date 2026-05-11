using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio2Repository
{
    private readonly LabDbContext _context;

    public Ejercicio2Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> Ejecutar()
    {
        return await _context.Products
            .Where(p => p.Price > 20)
            .ToListAsync();
    }
}