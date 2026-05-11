namespace PC_2JNC.Aplicacion.DTOs.Pedidos;

public sealed record PedidoResumenDto(
    int PedidoId,
    DateTime FechaPedido,
    int TotalItems,
    decimal TotalPedido);
