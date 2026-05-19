using Microsoft.EntityFrameworkCore;
using PC_2JNC.Aplicacion.DTOs.Clientes;
using PC_2JNC.Aplicacion.DTOs.Pedidos;
using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;

namespace PC_2JNC.Infraestructura.Data.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly LabDbContext _context;

    public ClienteRepository(LabDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<ClienteDto>> BuscarPorPrefijoAsync(
        string prefijo,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Clientes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(prefijo))
        {
            query = query.Where(cliente => cliente.Nombre.StartsWith(prefijo));
        }

        return await query
            .OrderBy(cliente => cliente.Nombre)
            .Select(cliente => new ClienteDto(cliente.ClienteId, cliente.Nombre, cliente.Email))
            .ToListAsync(cancellationToken);
    }

    public async Task<ClienteConPedidosDto?> ObtenerClienteConPedidosAsync(
        int clienteId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(cliente => cliente.ClienteId == clienteId)
            .Select(cliente => new ClienteConPedidosDto(
                cliente.ClienteId,
                cliente.Nombre,
                cliente.Email,
                cliente.Pedidos
                    .OrderByDescending(pedido => pedido.FechaPedido)
                    .Select(pedido => new PedidoResumenDto(
                        pedido.PedidoId,
                        pedido.FechaPedido,
                        pedido.DetallesPedido.Sum(detalle => detalle.Cantidad),
                        pedido.DetallesPedido.Sum(detalle => detalle.Cantidad * detalle.Producto.Precio)))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<string>> ObtenerNombresQueCompraronProductoAsync(
        int productoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Where(cliente => cliente.Pedidos.Any(pedido =>
                pedido.DetallesPedido.Any(detalle => detalle.ProductoId == productoId)))
            .OrderBy(cliente => cliente.Nombre)
            .Select(cliente => cliente.Nombre)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
