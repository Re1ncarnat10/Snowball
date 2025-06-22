namespace SnowBall.Interfaces;

using SnowBall.Dtos;

public interface IAdminService
{
    Task<SnowballDto> CreateSnowballAsync(SnowballDto snowballDto, IFormFile imageFile);
    Task UpdateSnowballAsync(int id, SnowballDto snowballDto, IFormFile imageFile);
    Task DeleteSnowballAsync(int id);
    Task InitializeAdminAsync();
}