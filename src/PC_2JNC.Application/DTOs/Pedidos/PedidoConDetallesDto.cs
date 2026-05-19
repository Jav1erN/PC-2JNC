namespace PC_2JNC.Aplicacion.DTOs.Pedidos;

public sealed record PedidoConDetallesDto(
    int PedidoId,
    int ClienteId,
    string NombreCliente,
    DateTime FechaPedido,
    IReadOnlyCollection<DetallePedidoDto> Detalles,
    decimal TotalPedido);
