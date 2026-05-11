using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio7Repository
{
    private readonly LabDbContext _context;

    public Ejercicio7Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> Ejecutar()
    {
        return await _context.Products
            .AverageAsync(p => p.Price);
    }
}