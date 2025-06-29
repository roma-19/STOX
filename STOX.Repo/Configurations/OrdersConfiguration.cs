using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STOX.Data.Entities;
using STOX.Data.Enums;

namespace STOX.Repo.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.OrderDate)
            .IsRequired();
        
        builder.Property(o => o.TotalPrice)
            .IsRequired()
            .HasPrecision(10, 2);
        
        builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending);

        builder.Property(o => o.DeliveryMethod)
            .HasMaxLength(50);
        
        builder.Property(o => o.PaymentMethod)
            .HasMaxLength(50);
        
        builder.HasOne(o => o.User)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(o => o.Handler)
            .WithMany(u => u.OrdersHandled)
            .HasForeignKey(o => o.HandlerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(o => o.Items)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}