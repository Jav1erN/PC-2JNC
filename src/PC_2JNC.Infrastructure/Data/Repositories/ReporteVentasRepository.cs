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
        var ventas = await ObtenerVentasAgrupadasAsync(cancellationToken);

        return ventas
            .OrderByDescending(resumen => resumen.TotalPedidos)
            .FirstOrDefault();
    }

    public async Task<IReadOnlyCollection<ResumenComprasClienteDto>> ObtenerResumenComprasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        // Primera consulta a la base de datos
        var detalles = await _context.DetallesPedido
            .AsNoTracking()
            .Include(detalle => detalle.Pedido)
                .ThenInclude(pedido => pedido.Cliente)
            .Include(detalle => detalle.Producto)
            .ToListAsync(cancellationToken);

        // Segunda consulta/procesamiento en memoria
        return detalles
            .GroupBy(detalle => new
            {
                detalle.Pedido.ClienteId,
                detalle.Pedido.Cliente.Nombre
            })

            .Select(grupo => new ResumenComprasClienteDto(
                grupo.Key.ClienteId,
                grupo.Key.Nombre,

                grupo.Select(detalle => detalle.PedidoId)
                    .Distinct()
                    .Count(),

                grupo.Sum(detalle => detalle.Cantidad),

                grupo.Sum(detalle =>
                    detalle.Cantidad * detalle.Producto.Precio)
            ))

            .OrderBy(resumen => resumen.NombreCliente)

            .ToList();
    }

    public async Task<IReadOnlyCollection<VentasAgrupadasPorClienteDto>> ObtenerVentasAgrupadasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        var ventas = await ObtenerVentasAgrupadasAsync(cancellationToken);

        return ventas
            .OrderByDescending(resumen => resumen.TotalVentas)
            .ToList();
    }

    private async Task<List<VentasAgrupadasPorClienteDto>>
        ObtenerVentasAgrupadasAsync(
        CancellationToken cancellationToken = default)
    {
        // Primera consulta a la BD
        var detalles = await _context.DetallesPedido
            .AsNoTracking()
            .Include(detalle => detalle.Pedido)
                .ThenInclude(pedido => pedido.Cliente)
            .Include(detalle => detalle.Producto)
            .ToListAsync(cancellationToken);

        // Segunda consulta/procesamiento en memoria
        return detalles
            .GroupBy(detalle => new
            {
                detalle.Pedido.ClienteId,
                detalle.Pedido.Cliente.Nombre
            })

            .Select(grupo => new VentasAgrupadasPorClienteDto(
                grupo.Key.ClienteId,
                grupo.Key.Nombre,

                grupo.Select(detalle => detalle.PedidoId)
                    .Distinct()
                    .Count(),

                grupo.Sum(detalle => detalle.Cantidad),

                grupo.Sum(detalle =>
                    detalle.Cantidad * detalle.Producto.Precio)
            ))

            .ToList();
    }
}