namespace SnowBall.Models;

public class Order
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<Snowball> Snowballs { get; set; }
}