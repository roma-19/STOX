using STOX.Data.Enums;

namespace STOX.Data.Entities;

public class Orders : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid HandlerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public string? DeliveryMethod { get; set; }
    public string? PaymentMethod { get; set; }
    public User User { get; set; }
    public User Handler { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}