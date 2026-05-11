using PC_2JNC.Aplicacion.DTOs.Comun;
using PC_2JNC.Aplicacion.DTOs.Pedidos;

namespace PC_2JNC.Aplicacion.Interfaces;

public interface IPedidoRepository
{
    Task<PedidoConDetallesDto?> ObtenerPedidoConDetallesAsync(int pedidoId, CancellationToken cancellationToken = default);
    Task<int> ObtenerCantidadProductosAsync(int pedidoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PedidoResumenDto>> ObtenerPedidosPosterioresAAsync(DateTime fecha, CancellationToken cancellationToken = default);
    Task<ResultadoPaginadoDto<DetallePedidoDto>> ObtenerDetallesPaginadosAsync(ConsultaPaginada consulta, CancellationToken cancellationToken = default);
}
