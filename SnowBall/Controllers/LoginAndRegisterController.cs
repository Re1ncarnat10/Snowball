using Microsoft.AspNetCore.Mvc;
using SnowBall.Interfaces;
using SnowBall.Dtos;

namespace SnowBall.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginAndRegisterController : ControllerBase
{
    private readonly ILoginAndRegisterService _service;

    public LoginAndRegisterController(ILoginAndRegisterService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var result = await _service.RegisterUserAsync(registerDto);
        if (result.Succeeded)
            return Ok();
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
    {
        try
        {
            var token = await _service.LoginUserAsync(loginDto);
            return Ok(token);
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRoles()
    {
        await _service.CreateRoles();
        return Ok();
    }
}