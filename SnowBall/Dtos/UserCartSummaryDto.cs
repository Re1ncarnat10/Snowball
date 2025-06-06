namespace SnowBall.Dtos;

public class UserCartSummaryDto
{
    public List<SnowballDto> Items { get; set; }
    public decimal TotalPrice { get; set; }
}