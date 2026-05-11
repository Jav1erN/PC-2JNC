using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio5Repository
{
    private readonly LabDbContext _context;

    public Ejercicio5Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Ejecutar()
    {
        return await _context.Products
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();
    }
}