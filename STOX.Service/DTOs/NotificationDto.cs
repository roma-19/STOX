namespace STOX.Service.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime NotificationDate { get; set; }
    public bool? IsRead { get; set; }
}