using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STOX.Data.Entities;

namespace STOX.Repo.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10, 2);
        
        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.Property(p => p.Category)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Discount)
            .HasDefaultValue(0);
        
        builder.Property(p => p.IsOnSale)
            .HasDefaultValue(false);
        
        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi =>  oi.ProductId);
        
        builder.HasMany(p => p.CartItems)
            .WithOne(ci => ci.Product)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}