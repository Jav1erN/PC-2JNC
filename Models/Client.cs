namespace PC_2JNC.Models;
public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public ICollection<Order> Orders { get; set; }
}
