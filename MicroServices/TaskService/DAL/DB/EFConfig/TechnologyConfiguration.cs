using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DB.EFConfig
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.ToTable("technology");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(t => t.Name)
                   .IsUnique();

            builder.HasMany(t => t.TaskTechnologies)
                   .WithOne(tt => tt.Technology)
                   .HasForeignKey(tt => tt.TechnologyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}