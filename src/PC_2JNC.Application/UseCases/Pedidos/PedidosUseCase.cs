using PC_2JNC.Aplicacion.DTOs.Comun;
using PC_2JNC.Aplicacion.DTOs.Pedidos;
using PC_2JNC.Aplicacion.Interfaces;

namespace PC_2JNC.Aplicacion.CasosDeUso.Pedidos;

public sealed class PedidosUseCase : IPedidosUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidosUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public Task<PedidoConDetallesDto?> ObtenerPedidoConDetallesAsync(
        int pedidoId,
        CancellationToken cancellationToken = default)
    {
        return _pedidoRepository.ObtenerPedidoConDetallesAsync(pedidoId, cancellationToken);
    }

    public Task<int> ObtenerCantidadProductosAsync(
        int pedidoId,
        CancellationToken cancellationToken = default)
    {
        return _pedidoRepository.ObtenerCantidadProductosAsync(pedidoId, cancellationToken);
    }

    public Task<IReadOnlyCollection<PedidoResumenDto>> ObtenerPedidosPosterioresAAsync(
        DateTime fecha,
        CancellationToken cancellationToken = default)
    {
        return _pedidoRepository.ObtenerPedidosPosterioresAAsync(fecha, cancellationToken);
    }

    public Task<ResultadoPaginadoDto<DetallePedidoDto>> ObtenerDetallesPaginadosAsync(
        ConsultaPaginada consulta,
        CancellationToken cancellationToken = default)
    {
        return _pedidoRepository.ObtenerDetallesPaginadosAsync(consulta, cancellationToken);
    }
}
