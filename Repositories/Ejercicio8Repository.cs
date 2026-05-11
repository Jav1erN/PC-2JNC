using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio8Repository
{
    private readonly LabDbContext _context;

    public Ejercicio8Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> Ejecutar()
    {
        return await _context.Products
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();
    }
}