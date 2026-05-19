namespace PC_2JNC.Domain.Entities;

public class DetallePedido
{
    public int DetallePedidoId { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }

    public Pedido Pedido { get; set; } = null!;
    public Producto Producto { get; set; } = null!;
}
