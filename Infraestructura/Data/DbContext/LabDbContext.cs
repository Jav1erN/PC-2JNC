using Microsoft.EntityFrameworkCore;
using PC_2JNC.Dominio.Entidades;

namespace PC_2JNC.Infraestructura.Data.DbContext;

public class LabDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
