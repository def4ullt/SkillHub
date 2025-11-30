using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB.EFConfig;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DB
{
    public class CatalogServiceDbContext : DbContext
    {
        public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options) : base(options)
        {

        }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TaskTechnology> TaskTechnologies { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TechnologyConfiguration());
            modelBuilder.ApplyConfiguration(new TaskTechnologyConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new TaskTagConfiguration());
        }
    }
}
