using Microsoft.AspNetCore.Mvc;
using PC_2JNC.Aplicacion.CasosDeUso.Clientes;
using PC_2JNC.Aplicacion.CasosDeUso.Pedidos;
using PC_2JNC.Aplicacion.CasosDeUso.Productos;
using PC_2JNC.Aplicacion.CasosDeUso.Reportes;
using PC_2JNC.Aplicacion.DTOs.Comun;

namespace PC_2JNC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController : ControllerBase
{
    private const string PrefijoClientePorDefecto = "Juan";
    private const decimal PrecioMinimoPorDefecto = 20m;
    private const int PedidoPorDefecto = 1;
    private const int ClientePorDefecto = 1;
    private const int ProductoPorDefecto = 2;

    private readonly IClientesUseCase _clientesUseCase;
    private readonly IProductosUseCase _productosUseCase;
    private readonly IPedidosUseCase _pedidosUseCase;
    private readonly IReportesUseCase _reportesUseCase;

    public OrdersController(
        IClientesUseCase clientesUseCase,
        IProductosUseCase productosUseCase,
        IPedidosUseCase pedidosUseCase,
        IReportesUseCase reportesUseCase)
    {
        _clientesUseCase = clientesUseCase;
        _productosUseCase = productosUseCase;
        _pedidosUseCase = pedidosUseCase;
        _reportesUseCase = reportesUseCase;
    }

    [HttpGet("ej1")]
    public async Task<IActionResult> Ej1(CancellationToken cancellationToken)
    {
        var clientes = await _clientesUseCase.BuscarPorPrefijoAsync(PrefijoClientePorDefecto, cancellationToken);
        return Ok(clientes);
    }

    [HttpGet("ej2")]
    public async Task<IActionResult> Ej2(CancellationToken cancellationToken)
    {
        var productos = await _productosUseCase.ObtenerPorPrecioMinimoAsync(PrecioMinimoPorDefecto, cancellationToken);
        return Ok(productos);
    }

    [HttpGet("ej3")]
    public async Task<IActionResult> Ej3(CancellationToken cancellationToken)
    {
        var pedido = await _pedidosUseCase.ObtenerPedidoConDetallesAsync(PedidoPorDefecto, cancellationToken);
        return pedido is null ? NotFound() : Ok(pedido.Detalles);
    }

    [HttpGet("ej4")]
    public async Task<IActionResult> Ej4(CancellationToken cancellationToken)
    {
        var total = await _pedidosUseCase.ObtenerCantidadProductosAsync(PedidoPorDefecto, cancellationToken);
        return Ok(total);
    }

    [HttpGet("ej5")]
    public async Task<IActionResult> Ej5(CancellationToken cancellationToken)
    {
        var producto = await _productosUseCase.ObtenerMasCaroAsync(cancellationToken);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpGet("ej6")]
    public async Task<IActionResult> Ej6([FromQuery] DateTime? fecha, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidosUseCase.ObtenerPedidosPosterioresAAsync(fecha ?? DateTime.MinValue, cancellationToken);
        return Ok(pedidos);
    }

    [HttpGet("ej7")]
    public async Task<IActionResult> Ej7(CancellationToken cancellationToken)
    {
        var promedio = await _productosUseCase.ObtenerPrecioPromedioAsync(cancellationToken);
        return Ok(promedio);
    }

    [HttpGet("ej8")]
    public async Task<IActionResult> Ej8(CancellationToken cancellationToken)
    {
        var productos = await _productosUseCase.ObtenerSinDescripcionAsync(cancellationToken);
        return Ok(productos);
    }

    [HttpGet("ej9")]
    public async Task<IActionResult> Ej9(CancellationToken cancellationToken)
    {
        var resumen = await _reportesUseCase.ObtenerClienteConMasPedidosAsync(cancellationToken);
        return resumen is null ? NotFound() : Ok(resumen);
    }

    [HttpGet("ej10")]
    public async Task<IActionResult> Ej10(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanoPagina = 20,
        CancellationToken cancellationToken = default)
    {
        var detalles = await _pedidosUseCase.ObtenerDetallesPaginadosAsync(
            new ConsultaPaginada(pagina, tamanoPagina),
            cancellationToken);

        return Ok(detalles);
    }

    [HttpGet("ej11")]
    public async Task<IActionResult> Ej11(CancellationToken cancellationToken)
    {
        var productos = await _productosUseCase.ObtenerProductosCompradosPorClienteAsync(ClientePorDefecto, cancellationToken);
        return Ok(productos);
    }

    [HttpGet("ej12")]
    public async Task<IActionResult> Ej12(CancellationToken cancellationToken)
    {
        var clientes = await _clientesUseCase.ObtenerClientesQueCompraronProductoAsync(ProductoPorDefecto, cancellationToken);
        return Ok(clientes);
    }

    [HttpGet("clientes/{clienteId:int}/pedidos")]
    public async Task<IActionResult> ObtenerClienteConPedidos(int clienteId, CancellationToken cancellationToken)
    {
        var cliente = await _clientesUseCase.ObtenerClienteConPedidosAsync(clienteId, cancellationToken);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpGet("pedidos/{pedidoId:int}/detalles")]
    public async Task<IActionResult> ObtenerPedidoConDetalles(int pedidoId, CancellationToken cancellationToken)
    {
        var pedido = await _pedidosUseCase.ObtenerPedidoConDetallesAsync(pedidoId, cancellationToken);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpGet("reportes/resumen-compras-clientes")]
    public async Task<IActionResult> ObtenerResumenComprasPorCliente(CancellationToken cancellationToken)
    {
        var resumen = await _reportesUseCase.ObtenerResumenComprasPorClienteAsync(cancellationToken);
        return Ok(resumen);
    }

    [HttpGet("reportes/ventas-por-cliente")]
    public async Task<IActionResult> ObtenerVentasAgrupadasPorCliente(CancellationToken cancellationToken)
    {
        var ventas = await _reportesUseCase.ObtenerVentasAgrupadasPorClienteAsync(cancellationToken);
        return Ok(ventas);
    }
}
