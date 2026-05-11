namespace PC_2JNC.Dominio.Entidades;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
