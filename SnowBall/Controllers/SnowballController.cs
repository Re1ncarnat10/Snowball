using Microsoft.AspNetCore.Mvc;
using SnowBall.Interfaces;
using SnowBall.Dtos;

namespace SnowBall.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SnowballController : ControllerBase
{
    private readonly ISnowballService _snowballService;

    public SnowballController(ISnowballService snowballService)
    {
        _snowballService = snowballService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SnowballDto>>> GetAll()
    {
        var snowballs = await _snowballService.GetAllSnowballsAsync();
        return Ok(snowballs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SnowballDto>> GetById(int id)
    {
        try
        {
            var snowball = await _snowballService.GetSnowballByIdAsync(id);
            return Ok(snowball);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}