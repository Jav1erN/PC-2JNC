namespace PC_2JNC.Aplicacion.DTOs.Pedidos;

public sealed record DetallePedidoDto(
    int ProductoId,
    string NombreProducto,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Subtotal);
