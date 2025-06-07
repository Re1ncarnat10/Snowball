using Microsoft.AspNetCore.Mvc;
using SnowBall.Interfaces;
using SnowBall.Dtos;

namespace SnowBall.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
  private readonly IOrderService _orderService;

  public OrderController(IOrderService orderService)
  {
    _orderService = orderService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
  {
    var orders = await _orderService.GetAllOrdersAsync();
    return Ok(orders);
  }

  [HttpGet("{orderId}")]
  public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
  {
    var order = await _orderService.GetOrderByIdAsync(orderId);
    return order != null ? Ok(order) : NotFound();
  }
}