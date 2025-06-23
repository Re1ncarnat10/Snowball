using Microsoft.EntityFrameworkCore;
using SnowBall.Data;
using SnowBall.Dtos;
using SnowBall.Interfaces;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Snowballs)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Snowballs = o.Snowballs.Select(s => new SnowballDto
                {
                    SnowballId = s.SnowballId,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Image = s.Image
                }).ToList()
            }).ToListAsync();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Snowballs)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null) return null;

        return new OrderDto
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Snowballs = order.Snowballs.Select(s => new SnowballDto
            {
                SnowballId = s.SnowballId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Image = s.Image
            }).ToList()
        };
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Include(o => o.Snowballs)
            .Where(o => o.UserId == userId)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Snowballs = o.Snowballs.Select(s => new SnowballDto
                {
                    SnowballId = s.SnowballId,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Image = s.Image
                }).ToList()
            }).ToListAsync();
    }
}