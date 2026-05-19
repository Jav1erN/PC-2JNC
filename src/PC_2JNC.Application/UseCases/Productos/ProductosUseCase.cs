using PC_2JNC.Aplicacion.DTOs.Productos;
using PC_2JNC.Aplicacion.Interfaces;

namespace PC_2JNC.Aplicacion.CasosDeUso.Productos;

public sealed class ProductosUseCase : IProductosUseCase
{
    private readonly IProductoRepository _productoRepository;

    public ProductosUseCase(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<IReadOnlyCollection<ProductoDto>> ObtenerPorPrecioMinimoAsync(
        decimal precioMinimo,
        CancellationToken cancellationToken = default)
    {
        return _productoRepository.ObtenerPorPrecioMinimoAsync(precioMinimo, cancellationToken);
    }

    public Task<ProductoDto?> ObtenerMasCaroAsync(CancellationToken cancellationToken = default)
    {
        return _productoRepository.ObtenerMasCaroAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<ProductoDto>> ObtenerSinDescripcionAsync(CancellationToken cancellationToken = default)
    {
        return _productoRepository.ObtenerSinDescripcionAsync(cancellationToken);
    }

    public Task<decimal> ObtenerPrecioPromedioAsync(CancellationToken cancellationToken = default)
    {
        return _productoRepository.ObtenerPrecioPromedioAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<string>> ObtenerProductosCompradosPorClienteAsync(
        int clienteId,
        CancellationToken cancellationToken = default)
    {
        return _productoRepository.ObtenerNombresCompradosPorClienteAsync(clienteId, cancellationToken);
    }
}
