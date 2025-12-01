using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
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
        private TaskServiceDbContext context;

        public TaskRepository(TaskServiceDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<(List<TaskEntity> Items, int TotalCount)> GetPagedTasksAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default)
        {
            var spec = new TaskWithFiltersSpecification(parameters);
            var query = context.Tasks.AsQueryable();

            var specEvaluator = new SpecificationEvaluator();
            var filteredQuery = specEvaluator.GetQuery(query, spec);

            var totalCount = await filteredQuery.CountAsync(cancellationToken);

            var items = await filteredQuery
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Tasks
                .Include(t => t.TaskTechnologies)
                    .ThenInclude(tt => tt.Technology)
                .Include(t => t.TaskTags)
                    .ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}
