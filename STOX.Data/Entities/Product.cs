using STOX.Data.Enums;

namespace STOX.Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }
    public string? Description { get; set; }
    public int Discount { get; set; }
    public bool IsOnSale  { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}