namespace SnowBall.Interfaces;

using SnowBall.Dtos;
using Microsoft.AspNetCore.Identity;

public interface IUserCartService
{
    Task<bool> AddSnowballToCartAsync(string userId, int snowballId);
    Task<bool> RemoveSnowballFromCartAsync(string userId, int snowballId);
    Task<UserCartSummaryDto> GetCartSummaryAsync(string userId);
    Task<bool> ClearCartAsync(string userId);
    Task<OrderDto> PlaceOrderAsync(string userId);
}