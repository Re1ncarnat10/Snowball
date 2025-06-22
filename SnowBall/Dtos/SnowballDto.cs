using Microsoft.AspNetCore.Mvc;

namespace SnowBall.Dtos;

public class SnowballDto
{
  [FromForm]
  public int SnowballId { get; set; }
  [FromForm]
  public string Name { get; set; }
  [FromForm]
  public string Description { get; set; }
  public string Image { get; set; }
  [FromForm]
  public decimal Price { get; set; }
}