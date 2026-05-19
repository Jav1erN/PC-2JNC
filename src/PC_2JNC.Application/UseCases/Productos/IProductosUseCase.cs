using PC_2JNC.Aplicacion.DTOs.Productos;

namespace PC_2JNC.Aplicacion.CasosDeUso.Productos;

public interface IProductosUseCase
{
    Task<IReadOnlyCollection<ProductoDto>> ObtenerPorPrecioMinimoAsync(decimal precioMinimo, CancellationToken cancellationToken = default);
    Task<ProductoDto?> ObtenerMasCaroAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProductoDto>> ObtenerSinDescripcionAsync(CancellationToken cancellationToken = default);
    Task<decimal> ObtenerPrecioPromedioAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<string>> ObtenerProductosCompradosPorClienteAsync(int clienteId, CancellationToken cancellationToken = default);
}
