using Microsoft.EntityFrameworkCore;
using SnowBall.Data;
using SnowBall.Dtos;
using SnowBall.Interfaces;

namespace SnowBall.Services;

public class SnowballService : ISnowballService
{
    private readonly AppDbContext _context;
    public SnowballService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SnowballDto>> GetAllSnowballsAsync()
    {
        return await _context.Snowballs
            .Select(s => new SnowballDto
            {
                SnowballId = s.SnowballId,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Image = s.Image
            })
            .ToListAsync();
    }

    public async Task<SnowballDto> GetSnowballByIdAsync(int id)
    {
        var snowball = await _context.Snowballs.FindAsync(id);
        if (snowball == null)
        {
            throw new KeyNotFoundException("Snowball not found");
        }

        return new SnowballDto
        {
            SnowballId = snowball.SnowballId,
            Name = snowball.Name,
            Description = snowball.Description,
            Price = snowball.Price,
            Image = snowball.Image
        };
    }
}