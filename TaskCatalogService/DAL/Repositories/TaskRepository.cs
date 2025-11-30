using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.Repositories.Interfaces;
using DAL.SpecificationPattern;
using Domain.Entities;
using Domain.Query;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
    {
        private CatalogServiceDbContext context;

        public TaskRepository(CatalogServiceDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetPagedTasksAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var spec = new TaskWithFiltersSpecification(parameters);
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Tasks
                .Include(t => t.TaskTechnologies)
                    .ThenInclude(tt => tt.Technology)
                .Include(t => t.TaskTags)
                    .ThenInclude(tt => tt.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}
