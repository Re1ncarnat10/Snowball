using Microsoft.EntityFrameworkCore;
using SnowBall.Data;
using SnowBall.Dtos;
using SnowBall.Interfaces;
using SnowBall.Models;

namespace SnowBall.Services;

public class UserCartService : IUserCartService
{
    private readonly AppDbContext _context;
    private readonly IOrderService _orderService;
    
    public UserCartService(AppDbContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }
    
    public async Task<bool> AddSnowballToCartAsync(string userId, int snowballId)
    {
        var userCart = await _context.UserCarts
                        .Include(uc => uc.Snowballs)
                        .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userCart == null)
        {
            userCart = new UserCart { UserId = userId, Snowballs = new List<Snowball>() };
            _context.UserCarts.Add(userCart);
        }

        var snowball = await _context.Snowballs.FindAsync(snowballId);
        if (snowball == null)
            return false;

        if (!userCart.Snowballs.Contains(snowball))
            userCart.Snowballs.Add(snowball);

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> RemoveSnowballFromCartAsync(string userId, int snowballId)
    {
        var userCart = await _context.UserCarts
            .Include(uc => uc.Snowballs)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userCart == null)
            return false;

        var snowball = userCart.Snowballs.FirstOrDefault(s => s.SnowballId == snowballId);
        if (snowball == null)
            return false;

        userCart.Snowballs.Remove(snowball);

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<UserCartSummaryDto> GetCartSummaryAsync(string userId)
    {
        var userCart = await _context.UserCarts
            .Include(uc => uc.Snowballs)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userCart == null)
            return null;

        var items = userCart.Snowballs
            .Select(s => new SnowballDto
            {
                SnowballId = s.SnowballId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Image = s.Image
            }).ToList();

        var total = items.Sum(i => i.Price);

        return new UserCartSummaryDto
        {
            Items = items,
            TotalPrice = total
        };
    }
    
    public async Task<bool> ClearCartAsync(string userId)
    {
        var userCart = await _context.UserCarts
            .Include(uc => uc.Snowballs)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userCart == null)
            return false;

        userCart.Snowballs.Clear();

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<OrderDto> PlaceOrderAsync(string userId)
    {
        var userCart = await _context.UserCarts
                        .Include(uc => uc.Snowballs)
                        .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userCart == null || !userCart.Snowballs.Any())
            return null;

        var order = new Order
        {
                        UserId = userId,
                        OrderDate = DateTime.UtcNow,
                        TotalPrice = userCart.Snowballs.Sum(s => s.Price),
                        Snowballs = new List<Snowball>(userCart.Snowballs)
        };

        _context.Orders.Add(order);
        userCart.Snowballs.Clear();
        await _context.SaveChangesAsync();

        var orderDto = new OrderDto
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

        return orderDto;
    }
}