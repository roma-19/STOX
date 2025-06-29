using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STOX.Data.Entities;

namespace STOX.Repo.Configurations;

public class CartItemConfiguration :  IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cartitem");
        
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasPrecision(10, 2);
        
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}