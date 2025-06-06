using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnowBall.Models;

public class Snowball
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SnowballId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string Image { get; set; }
    [Required]
    public decimal Price { get; set; }
    public ICollection<UserCart> UserCarts { get; set; }
    public ICollection<Order> Orders { get; set; }
}