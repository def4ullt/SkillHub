using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.DB.EFConfig
{
    public class TaskTechnologyConfiguration : IEntityTypeConfiguration<TaskTechnology>
    {
        public void Configure(EntityTypeBuilder<TaskTechnology> builder)
        {
            builder.ToTable("task-technology");

            builder.HasKey(tt => new { tt.TaskId, tt.TechnologyId });

            builder.HasOne(tt => tt.Task)
                   .WithMany(t => t.TaskTechnologies)
                   .HasForeignKey(tt => tt.TaskId);

            builder.HasOne(tt => tt.Technology)
                   .WithMany(t => t.TaskTechnologies)
                   .HasForeignKey(tt => tt.TechnologyId);
        }
    }
}
