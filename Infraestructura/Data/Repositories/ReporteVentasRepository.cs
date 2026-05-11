using Microsoft.EntityFrameworkCore;
using PC_2JNC.Aplicacion.DTOs.Reportes;
using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;

namespace PC_2JNC.Infraestructura.Data.Repositories;

public sealed class ReporteVentasRepository : IReporteVentasRepository
{
    private readonly LabDbContext _context;

    public ReporteVentasRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<VentasAgrupadasPorClienteDto?> ObtenerClienteConMasPedidosAsync(
        CancellationToken cancellationToken = default)
    {
        return await ObtenerVentasAgrupadasQuery()
            .OrderByDescending(resumen => resumen.TotalPedidos)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ResumenComprasClienteDto>> ObtenerResumenComprasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.DetallesPedido
            .AsNoTracking()
            .GroupBy(detalle => new
            {
                detalle.Pedido.ClienteId,
                detalle.Pedido.Cliente.Nombre
            })
            .Select(grupo => new ResumenComprasClienteDto(
                grupo.Key.ClienteId,
                grupo.Key.Nombre,
                grupo.Select(detalle => detalle.PedidoId).Distinct().Count(),
                grupo.Sum(detalle => detalle.Cantidad),
                grupo.Sum(detalle => detalle.Cantidad * detalle.Producto.Precio)))
            .OrderBy(resumen => resumen.NombreCliente)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<VentasAgrupadasPorClienteDto>> ObtenerVentasAgrupadasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        return await ObtenerVentasAgrupadasQuery()
            .OrderByDescending(resumen => resumen.TotalVentas)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<VentasAgrupadasPorClienteDto> ObtenerVentasAgrupadasQuery()
    {
        return _context.DetallesPedido
            .AsNoTracking()
            .GroupBy(detalle => new
            {
                detalle.Pedido.ClienteId,
                detalle.Pedido.Cliente.Nombre
            })
            .Select(grupo => new VentasAgrupadasPorClienteDto(
                grupo.Key.ClienteId,
                grupo.Key.Nombre,
                grupo.Select(detalle => detalle.PedidoId).Distinct().Count(),
                grupo.Sum(detalle => detalle.Cantidad),
                grupo.Sum(detalle => detalle.Cantidad * detalle.Producto.Precio)));
    }
}
