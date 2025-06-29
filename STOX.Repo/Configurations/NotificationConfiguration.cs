using Microsoft.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using STOX.Data.Entities;
 
 namespace STOX.Repo.Configurations;
 
 public class NotificationConfiguration :  IEntityTypeConfiguration<Notification>
 {
     public void Configure(EntityTypeBuilder<Notification> builder)
     {
         builder.ToTable("notification");
         
         builder.HasKey(x => x.Id);
         
         builder.Property(n => n.Message)
             .IsRequired()
             .HasMaxLength(500);
         
         builder.Property(n => n.NotificationDate)
             .IsRequired();
 
         builder.Property(n => n.IsRead)
             .HasDefaultValue(false);
         
         builder.HasOne(n => n.User)
             .WithMany(u =>  u.Notifications)
             .HasForeignKey(n => n.UserId)
             .OnDelete(DeleteBehavior.Cascade);
     }
 }