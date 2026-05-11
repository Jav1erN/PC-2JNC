using PC_2JNC.Aplicacion.DTOs.Clientes;
using PC_2JNC.Aplicacion.Interfaces;

namespace PC_2JNC.Aplicacion.CasosDeUso.Clientes;

public sealed class ClientesUseCase : IClientesUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public ClientesUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public Task<IReadOnlyCollection<ClienteDto>> BuscarPorPrefijoAsync(
        string prefijo,
        CancellationToken cancellationToken = default)
    {
        return _clienteRepository.BuscarPorPrefijoAsync(prefijo.Trim(), cancellationToken);
    }

    public Task<ClienteConPedidosDto?> ObtenerClienteConPedidosAsync(
        int clienteId,
        CancellationToken cancellationToken = default)
    {
        return _clienteRepository.ObtenerClienteConPedidosAsync(clienteId, cancellationToken);
    }

    public Task<IReadOnlyCollection<string>> ObtenerClientesQueCompraronProductoAsync(
        int productoId,
        CancellationToken cancellationToken = default)
    {
        return _clienteRepository.ObtenerNombresQueCompraronProductoAsync(productoId, cancellationToken);
    }
}
