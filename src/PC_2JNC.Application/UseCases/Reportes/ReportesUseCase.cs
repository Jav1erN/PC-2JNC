using PC_2JNC.Aplicacion.DTOs.Reportes;
using PC_2JNC.Aplicacion.Interfaces;

namespace PC_2JNC.Aplicacion.CasosDeUso.Reportes;

public sealed class ReportesUseCase : IReportesUseCase
{
    private readonly IReporteVentasRepository _reporteVentasRepository;

    public ReportesUseCase(IReporteVentasRepository reporteVentasRepository)
    {
        _reporteVentasRepository = reporteVentasRepository;
    }

    public Task<VentasAgrupadasPorClienteDto?> ObtenerClienteConMasPedidosAsync(
        CancellationToken cancellationToken = default)
    {
        return _reporteVentasRepository.ObtenerClienteConMasPedidosAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<ResumenComprasClienteDto>> ObtenerResumenComprasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        return _reporteVentasRepository.ObtenerResumenComprasPorClienteAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<VentasAgrupadasPorClienteDto>> ObtenerVentasAgrupadasPorClienteAsync(
        CancellationToken cancellationToken = default)
    {
        return _reporteVentasRepository.ObtenerVentasAgrupadasPorClienteAsync(cancellationToken);
    }
}
