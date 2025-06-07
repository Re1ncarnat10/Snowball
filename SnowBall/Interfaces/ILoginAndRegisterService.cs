using SnowBall.Dtos;

namespace SnowBall.Interfaces;
using Microsoft.AspNetCore.Identity;

public interface ILoginAndRegisterService
{
    Task CreateRoles();
    Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
    Task<TokenDto> LoginUserAsync(LoginDto loginDto);
}