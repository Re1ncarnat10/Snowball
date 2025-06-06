using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnowBall.Models;

namespace SnowBall.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Snowball> Snowballs { get; set; }
    public DbSet<UserCart> UserCarts { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserCart)
            .WithOne(c => c.User)
            .HasForeignKey<UserCart>(c => c.UserId);

        modelBuilder.Entity<UserCart>()
            .HasMany(uc => uc.Snowballs)
            .WithMany(s => s.UserCarts)
            .UsingEntity(j => j.ToTable("UserCartSnowballs"));
    }
}