using Microsoft.EntityFrameworkCore;
using STOX.Data.Entities;
using STOX.Repo.Configurations;

namespace STOX.Repo;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrdersConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) :  base(options) {}
}