using Microsoft.AspNetCore.Mvc;
using SnowBall.Interfaces;
using SnowBall.Dtos;

namespace SnowBall.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCartController : ControllerBase
{
    private readonly IUserCartService _userCartService;

    public UserCartController(IUserCartService userCartService)
    {
        _userCartService = userCartService;
    }

    [HttpPost("{userId}/add/{snowballId}")]
    public async Task<IActionResult> AddToCart(string userId, int snowballId)
    {
        var result = await _userCartService.AddSnowballToCartAsync(userId, snowballId);
        return result ? Ok() : NotFound();
    }

    [HttpPost("{userId}/remove/{snowballId}")]
    public async Task<IActionResult> RemoveFromCart(string userId, int snowballId)
    {
        var result = await _userCartService.RemoveSnowballFromCartAsync(userId, snowballId);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{userId}/summary")]
    public async Task<ActionResult<UserCartSummaryDto>> GetCartSummary(string userId)
    {
        var summary = await _userCartService.GetCartSummaryAsync(userId);
        return summary != null ? Ok(summary) : NotFound();
    }

    [HttpPost("{userId}/clear")]
    public async Task<IActionResult> ClearCart(string userId)
    {
        var result = await _userCartService.ClearCartAsync(userId);
        return result ? Ok() : NotFound();
    }

    [HttpPost("{userId}/order")]
    public async Task<ActionResult<OrderDto>> PlaceOrder(string userId)
    {
        var order = await _userCartService.PlaceOrderAsync(userId);
        return order != null ? Ok(order) : BadRequest();
    }
}