namespace PC_2JNC.Models;

public class Order
{
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public DateTime OrderDate { get; set; }

    public Client Client { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}