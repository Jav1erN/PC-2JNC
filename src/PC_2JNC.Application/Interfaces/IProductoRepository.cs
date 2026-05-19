using PC_2JNC.Aplicacion.DTOs.Productos;

namespace PC_2JNC.Aplicacion.Interfaces;

public interface IProductoRepository
{
    Task<IReadOnlyCollection<ProductoDto>> ObtenerPorPrecioMinimoAsync(decimal precioMinimo, CancellationToken cancellationToken = default);
    Task<ProductoDto?> ObtenerMasCaroAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProductoDto>> ObtenerSinDescripcionAsync(CancellationToken cancellationToken = default);
    Task<decimal> ObtenerPrecioPromedioAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<string>> ObtenerNombresCompradosPorClienteAsync(int clienteId, CancellationToken cancellationToken = default);
}
