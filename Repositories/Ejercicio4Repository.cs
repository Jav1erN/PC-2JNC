using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio4Repository
{
    private readonly LabDbContext _context;

    public Ejercicio4Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<int> Ejecutar()
    {
        return await _context.OrderDetails
            .Where(d => d.OrderId == 1)
            .SumAsync(d => d.Quantity);
    }
}