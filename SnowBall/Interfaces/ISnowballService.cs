namespace SnowBall.Interfaces;

using SnowBall.Dtos;

public interface ISnowballService
{
    Task<IEnumerable<SnowballDto>> GetAllSnowballsAsync();
    Task<SnowballDto> GetSnowballByIdAsync(int id);
}