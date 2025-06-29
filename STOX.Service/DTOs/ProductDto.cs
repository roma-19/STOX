namespace STOX.Service.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
    public int Discount { get; set; }
    public bool IsOnSale { get; set; }
}