using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STOX.Data.Entities;

namespace STOX.Repo.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("review");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Comment)
            .HasMaxLength(1000);
        
        builder.Property(r => r.Rating)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.Property(r => r.ReviewDate)
            .IsRequired();
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Product)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}