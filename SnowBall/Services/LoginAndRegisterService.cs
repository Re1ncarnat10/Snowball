using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SnowBall.Dtos;
using SnowBall.Interfaces;
using SnowBall.Models;

namespace SnowBall.Services;

public class LoginAndRegisterService : ILoginAndRegisterService
{
 private readonly UserManager<User> _userManager;
 private readonly IConfiguration _configuration;
 private readonly RoleManager<IdentityRole> _roleManager;
 
 public LoginAndRegisterService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
 {
     _userManager = userManager;
     _configuration = configuration;
     _roleManager = roleManager;
 }

 public async Task CreateRoles()
 {
     string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
 }
    public async Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            Name = registerDto.Name,
            Wallet = 100
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return result;
        }
        
        var roleResult = await _userManager.AddToRoleAsync(user, "User");
        return !roleResult.Succeeded
            ? IdentityResult.Failed(new IdentityError { Description = "Failed to assign role to user." })
            : IdentityResult.Success;
    }

    public async Task<TokenDto> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid login attempt.");
            
        }
        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid login attempt.");
        }
        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwtKey = _configuration["Jwt:Key"];
        var key = Encoding.UTF8.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiresInMinutes"])),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateEncodedJwt(tokenDescriptor);

        return new TokenDto { Token = jwtToken }; 
    }
}