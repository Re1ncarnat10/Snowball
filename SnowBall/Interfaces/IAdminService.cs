namespace SnowBall.Interfaces;

using SnowBall.Dtos;

public interface IAdminService
{
    Task<SnowballDto> CreateSnowballAsync(SnowballDto snowballDto);
    Task UpdateSnowballAsync(int id, SnowballDto snowballDto);
    Task DeleteSnowballAsync(int id);
    Task InitializeAdminAsync();
}