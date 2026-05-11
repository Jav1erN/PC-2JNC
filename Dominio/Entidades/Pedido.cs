namespace PC_2JNC.Dominio.Entidades;

public class Pedido
{
    public int PedidoId { get; set; }
    public int ClienteId { get; set; }
    public DateTime FechaPedido { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
}
