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
    public class TaskTagConfiguration : IEntityTypeConfiguration<TaskTag>
    {
        public void Configure(EntityTypeBuilder<TaskTag> builder)
        {
            builder.ToTable("task_tag");

            builder.HasKey(tt => new { tt.TaskId, tt.TagId });

            builder.HasOne(tt => tt.Task)
                   .WithMany(t => t.TaskTags)
                   .HasForeignKey(tt => tt.TaskId);

            builder.HasOne(tt => tt.Tag)
                   .WithMany(t => t.TaskTags)
                   .HasForeignKey(tt => tt.TagId);
        }
    }
}
