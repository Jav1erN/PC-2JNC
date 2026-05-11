namespace PC_2JNC.Dominio.Entidades;

public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }

    public ICollection<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
}
