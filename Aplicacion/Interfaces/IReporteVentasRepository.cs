using PC_2JNC.Aplicacion.DTOs.Reportes;

namespace PC_2JNC.Aplicacion.Interfaces;

public interface IReporteVentasRepository
{
    Task<VentasAgrupadasPorClienteDto?> ObtenerClienteConMasPedidosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ResumenComprasClienteDto>> ObtenerResumenComprasPorClienteAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<VentasAgrupadasPorClienteDto>> ObtenerVentasAgrupadasPorClienteAsync(CancellationToken cancellationToken = default);
}
