using SnowBall.Dtos;
using SnowBall.Interfaces;

namespace SnowBall.Services;

public class OrderService : IOrderService
{
  private readonly List<OrderDto> _orders = new();

  public Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    => Task.FromResult(_orders.AsEnumerable());

  public Task<OrderDto?> GetOrderByIdAsync(int orderId)
    => Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == orderId));

  public Task AddOrderAsync(OrderDto order)
  {
    _orders.Add(order);
    return Task.CompletedTask;
  }
  public Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
  {
    var userOrders = _orders.Where(o => o.UserId == userId);
    return Task.FromResult(userOrders.AsEnumerable());
  }
}