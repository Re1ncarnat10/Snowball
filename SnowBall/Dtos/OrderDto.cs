﻿namespace SnowBall.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<SnowballDto> Snowballs { get; set; }
}