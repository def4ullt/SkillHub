using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DB.EFConfig
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("task");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200); 

            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(t => t.Difficulty)
                   .IsRequired();

            builder.Property(t => t.EstimatedTimeMinutes)
                   .IsRequired();

            builder.Property(t => t.XpReward)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .HasConversion(
                       v => v.ToUniversalTime(),
                       v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                   .IsRequired();

            builder.Property(t => t.UpdatedAt)
                   .HasConversion(
                       v => v.ToUniversalTime(),
                       v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                   .IsRequired();

            builder.Property(t => t.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasMany(t => t.TaskTechnologies)
                   .WithOne(tt => tt.Task)
                   .HasForeignKey(tt => tt.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.TaskTags)
                   .WithOne(tt => tt.Task)
                   .HasForeignKey(tt => tt.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}