using SnowBall.Dtos;

namespace SnowBall.Interfaces;

public interface IOrderService
{
  Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
  Task<OrderDto?> GetOrderByIdAsync(int orderId);
  Task AddOrderAsync(OrderDto order);
  Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
}