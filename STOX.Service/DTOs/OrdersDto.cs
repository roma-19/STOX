namespace STOX.Service.DTOs;

public class OrdersDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid HandlerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Status { get; set; }
    public string? DeliveryMethod { get; set; }
    public string? PaymentMethod { get; set; }
}