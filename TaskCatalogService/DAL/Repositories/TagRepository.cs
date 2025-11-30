using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private CatalogServiceDbContext context;

        public TagRepository(CatalogServiceDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await context.Tags
                .AsNoTracking()
                .AnyAsync(t => t.Name.ToLower() == name.ToLower()
                               && (!excludeId.HasValue || t.Id != excludeId.Value),
                          cancellationToken);
        }
    }
}
