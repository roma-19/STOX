namespace STOX.Data.Entities;

public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}