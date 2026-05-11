using PC_2JNC.Aplicacion.DTOs.Clientes;

namespace PC_2JNC.Aplicacion.CasosDeUso.Clientes;

public interface IClientesUseCase
{
    Task<IReadOnlyCollection<ClienteDto>> BuscarPorPrefijoAsync(string prefijo, CancellationToken cancellationToken = default);
    Task<ClienteConPedidosDto?> ObtenerClienteConPedidosAsync(int clienteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<string>> ObtenerClientesQueCompraronProductoAsync(int productoId, CancellationToken cancellationToken = default);
}
