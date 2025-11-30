using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Query;

namespace DAL.Repositories.Interfaces
{
    public interface ITaskRepository : IGenericRepository<TaskEntity>
    {
        Task<(List<TaskEntity> Items, int TotalCount)> GetPagedTasksAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default);
        Task<TaskEntity?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
