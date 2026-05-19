using Microsoft.EntityFrameworkCore;
using PC_2JNC.Aplicacion.DTOs.Productos;
using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;

namespace PC_2JNC.Infraestructura.Data.Repositories;

public sealed class ProductoRepository : IProductoRepository
{
    private readonly LabDbContext _context;

    public ProductoRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ProductoDto>> ObtenerPorPrecioMinimoAsync(
        decimal precioMinimo,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Productos.AsNoTracking();

        if (precioMinimo > 0)
        {
            query = query.Where(producto => producto.Precio > precioMinimo);
        }

        return await query
            .OrderBy(producto => producto.Nombre)
            .Select(producto => new ProductoDto(
                producto.ProductoId,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductoDto?> ObtenerMasCaroAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Productos
            .AsNoTracking()
            .OrderByDescending(producto => producto.Precio)
            .Select(producto => new ProductoDto(
                producto.ProductoId,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProductoDto>> ObtenerSinDescripcionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Productos
            .AsNoTracking()
            .Where(producto => string.IsNullOrEmpty(producto.Descripcion))
            .OrderBy(producto => producto.Nombre)
            .Select(producto => new ProductoDto(
                producto.ProductoId,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio))
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> ObtenerPrecioPromedioAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Productos
            .AsNoTracking()
            .AverageAsync(producto => producto.Precio, cancellationToken);
    }

    public async Task<IReadOnlyCollection<string>> ObtenerNombresCompradosPorClienteAsync(
        int clienteId,
        CancellationToken cancellationToken = default)
    {
        return await _context.DetallesPedido
            .AsNoTracking()
            .Where(detalle => detalle.Pedido.ClienteId == clienteId)
            .OrderBy(detalle => detalle.Producto.Nombre)
            .Select(detalle => detalle.Producto.Nombre)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
