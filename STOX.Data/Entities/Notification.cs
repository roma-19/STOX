namespace STOX.Data.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime NotificationDate { get; set; }
    public bool IsRead { get; set; }
    public User User { get; set; }
}