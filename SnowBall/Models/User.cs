using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SnowBall.Models;

public class User : IdentityUser
{
    public String Name { get; set; }
    public decimal Wallet { get; set; }
    public virtual UserCart UserCart { get; set; }
    public ICollection<Order> Orders { get; set; }
}
