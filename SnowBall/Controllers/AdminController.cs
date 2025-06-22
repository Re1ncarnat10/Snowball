using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnowBall.Interfaces;
using SnowBall.Dtos;

namespace SnowBall.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
  private readonly IAdminService _adminService;

  public AdminController(IAdminService adminService)
  {
    _adminService = adminService;
  }

  [HttpPost("snowball")]
  public async Task<ActionResult<SnowballDto>> CreateSnowball([FromForm] SnowballDto snowballDto, IFormFile imageFile)
  {
    var created = await _adminService.CreateSnowballAsync(snowballDto, imageFile);
    return CreatedAtAction(nameof(CreateSnowball), new { id = created.SnowballId }, created);
  }

  [HttpPut("snowball/{id}")]
  public async Task<IActionResult> UpdateSnowball(int id, [FromForm] SnowballDto snowballDto, IFormFile imageFile)
  {
    try
    {
      await _adminService.UpdateSnowballAsync(id, snowballDto, imageFile);
      return NoContent();
    }
    catch (Exception)
    {
      return NotFound();
    }
  }

  [HttpDelete("snowball/{id}")]
  public async Task<IActionResult> DeleteSnowball(int id)
  {
    try
    {
      await _adminService.DeleteSnowballAsync(id);
      return NoContent();
    }
    catch (Exception)
    {
      return NotFound();
    }
  }

  [HttpPost("initialize")]
  public async Task<IActionResult> InitializeAdmin()
  {
    await _adminService.InitializeAdminAsync();
    return Ok();
  }
}