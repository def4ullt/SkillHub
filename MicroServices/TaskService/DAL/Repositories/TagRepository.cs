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
        private TaskServiceDbContext context;

        public TagRepository(TaskServiceDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> AreTagsValidAsync(IEnumerable<Guid> tagIds, CancellationToken cancellationToken = default)
        {
            IEnumerable<Guid> distinctIds = tagIds.Distinct(); 
            List<Guid> existingIds = await context.Tags
                .Where(t => distinctIds.Contains(t.Id))
                .Select(t => t.Id)
                .ToListAsync(cancellationToken);

            return existingIds.Count == distinctIds.Count();
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
