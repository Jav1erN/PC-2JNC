using PC_2JNC.Aplicacion.DTOs.Clientes;

namespace PC_2JNC.Aplicacion.Interfaces;

public interface IClienteRepository
{
    Task<IReadOnlyCollection<ClienteDto>> BuscarPorPrefijoAsync(string prefijo, CancellationToken cancellationToken = default);
    Task<ClienteConPedidosDto?> ObtenerClienteConPedidosAsync(int clienteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<string>> ObtenerNombresQueCompraronProductoAsync(int productoId, CancellationToken cancellationToken = default);
}
