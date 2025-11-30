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
    public class TechnologyRepository : GenericRepository<Technology>, ITechnologyRepository
    {
        private CatalogServiceDbContext context;

        public TechnologyRepository(CatalogServiceDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> AreTechnologiesValidAsync(IEnumerable<Guid> technologyIds, CancellationToken cancellationToken = default)
        {
            List<Guid> existingIds = await context.Technologies
                .Where(t => technologyIds.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync(cancellationToken);

            return existingIds.Count == technologyIds.Count();
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await context.Technologies
                .AsNoTracking()
                .AnyAsync(t => t.Name.ToLower() == name.ToLower()
                               && (!excludeId.HasValue || t.Id != excludeId.Value),
                          cancellationToken);
        }
    }
}
