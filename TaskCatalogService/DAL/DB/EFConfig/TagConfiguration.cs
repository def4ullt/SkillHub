using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DB.EFConfig
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tag");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(t => t.Name)
                   .IsUnique();

            builder.HasMany(t => t.TaskTags)
                   .WithOne(tt => tt.Tag)
                   .HasForeignKey(tt => tt.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}