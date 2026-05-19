using PC_2JNC.Aplicacion.DTOs.Reportes;

namespace PC_2JNC.Aplicacion.CasosDeUso.Reportes;

public interface IReportesUseCase
{
    Task<VentasAgrupadasPorClienteDto?> ObtenerClienteConMasPedidosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ResumenComprasClienteDto>> ObtenerResumenComprasPorClienteAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<VentasAgrupadasPorClienteDto>> ObtenerVentasAgrupadasPorClienteAsync(CancellationToken cancellationToken = default);
}
