using SnowBall.Dtos;

namespace SnowBall.Interfaces;

public interface IOrderService
{
  Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
  Task<OrderDto?> GetOrderByIdAsync(int orderId);
  Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
}