using STOX.Data.Enums;

namespace STOX.Data.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public ICollection<Orders> Orders { get; set; } =  new List<Orders>();
    public ICollection<Orders> OrdersHandled { get; set; }  = new List<Orders>();
    public Cart Cart { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}