using Microsoft.AspNetCore.Identity;
using SnowBall.Data;
using SnowBall.Dtos;
using SnowBall.Interfaces;
using SnowBall.Models;

namespace SnowBall.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    
    public AdminService(UserManager<User> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<SnowballDto> CreateSnowballAsync(SnowballDto snowballDto)
    {
        var snowball = new Snowball
        {
            Name = snowballDto.Name,
            Description = snowballDto.Description,
            Price = snowballDto.Price,
            Image = snowballDto.Image,
        };
        
        _context.Snowballs.Add(snowball);
        await _context.SaveChangesAsync();
        
        snowballDto.SnowballId = snowball.SnowballId;
        return snowballDto;
    }

    public async Task UpdateSnowballAsync(int id, SnowballDto snowballDto)
    {
        var snowball = await _context.Snowballs.FindAsync(id);
        if (snowball == null)
        {
            throw new Exception("Snowball not found");
        }
        snowball.Name = snowballDto.Name;
        snowball.Description = snowballDto.Description;
        snowball.Price = snowballDto.Price;
        snowball.Image = snowballDto.Image;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSnowballAsync(int id)
    {
        var snowball = await _context.Snowballs.FindAsync(id);
        if (snowball == null)
        {
            throw new Exception("Snowball not found");
        }
        _context.Snowballs.Remove(snowball);
        await _context.SaveChangesAsync();
    }

    public async Task InitializeAdminAsync()
    {
        const string adminEmail = "admin@gmail.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser != null)
        {
            return;
        }

        adminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Admin",
        };
        var createUserResult = await _userManager.CreateAsync(adminUser, "Admin123!");
        if (!createUserResult.Succeeded)
        {
            return;
        }

        await _userManager.AddToRoleAsync(adminUser, "Admin");
        await _userManager.AddToRoleAsync(adminUser, "User");
    }
}