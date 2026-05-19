using Microsoft.EntityFrameworkCore;
using PC_2JNC.Aplicacion.DTOs.Comun;
using PC_2JNC.Aplicacion.DTOs.Pedidos;
using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;
using PC_2JNC.Infraestructura.LINQ;

namespace PC_2JNC.Infraestructura.Data.Repositories;

public sealed class PedidoRepository : IPedidoRepository
{
    private readonly LabDbContext _context;

    public PedidoRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<PedidoConDetallesDto?> ObtenerPedidoConDetallesAsync(
        int pedidoId,
        CancellationToken cancellationToken = default)
    {
        var pedido = await _context.Pedidos
            .AsNoTracking()
            .Include(pedido => pedido.Cliente)
            .Include(pedido => pedido.DetallesPedido)
                .ThenInclude(detalle => detalle.Producto)
            .FirstOrDefaultAsync(pedido => pedido.PedidoId == pedidoId, cancellationToken);

        if (pedido is null)
        {
            return null;
        }

        var detalles = pedido.DetallesPedido
            .OrderBy(detalle => detalle.Producto.Nombre)
            .Select(detalle => new DetallePedidoDto(
                detalle.ProductoId,
                detalle.Producto.Nombre,
                detalle.Cantidad,
                detalle.Producto.Precio,
                detalle.Cantidad * detalle.Producto.Precio))
            .ToList();

        return new PedidoConDetallesDto(
            pedido.PedidoId,
            pedido.ClienteId,
            pedido.Cliente.Nombre,
            pedido.FechaPedido,
            detalles,
            detalles.Sum(detalle => detalle.Subtotal));
    }

    public async Task<int> ObtenerCantidadProductosAsync(int pedidoId, CancellationToken cancellationToken = default)
    {
        return await _context.DetallesPedido
            .AsNoTracking()
            .Where(detalle => detalle.PedidoId == pedidoId)
            .SumAsync(detalle => detalle.Cantidad, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PedidoResumenDto>> ObtenerPedidosPosterioresAAsync(
        DateTime fecha,
        CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .AsNoTracking()
            .Where(pedido => pedido.FechaPedido > fecha)
            .OrderByDescending(pedido => pedido.FechaPedido)
            .Select(pedido => new PedidoResumenDto(
                pedido.PedidoId,
                pedido.FechaPedido,
                pedido.DetallesPedido.Sum(detalle => detalle.Cantidad),
                pedido.DetallesPedido.Sum(detalle => detalle.Cantidad * detalle.Producto.Precio)))
            .ToListAsync(cancellationToken);
    }

    public async Task<ResultadoPaginadoDto<DetallePedidoDto>> ObtenerDetallesPaginadosAsync(
        ConsultaPaginada consulta,
        CancellationToken cancellationToken = default)
    {
        var query =
            from detalle in _context.DetallesPedido.AsNoTracking()
            join producto in _context.Productos.AsNoTracking()
                on detalle.ProductoId equals producto.ProductoId
            orderby detalle.PedidoId, producto.Nombre
            select new DetallePedidoDto(
                producto.ProductoId,
                producto.Nombre,
                detalle.Cantidad,
                producto.Precio,
                detalle.Cantidad * producto.Precio);

        var totalRegistros = await query.CountAsync(cancellationToken);
        var items = await query
            .Paginar(consulta.PaginaNormalizada, consulta.TamanoPaginaNormalizado)
            .ToListAsync(cancellationToken);

        return new ResultadoPaginadoDto<DetallePedidoDto>(
            items,
            totalRegistros,
            consulta.PaginaNormalizada,
            consulta.TamanoPaginaNormalizado);
    }
}
