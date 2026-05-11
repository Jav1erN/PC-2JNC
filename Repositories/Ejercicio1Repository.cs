using Microsoft.EntityFrameworkCore;
using PC_2JNC.Models;

public class Ejercicio1Repository
{
    private readonly LabDbContext _context;

    public Ejercicio1Repository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> Ejecutar()
    {
        return await _context.Clients
            .Where(c => c.Name.StartsWith("Juan"))
            .ToListAsync();
    }
}