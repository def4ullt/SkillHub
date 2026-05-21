using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using Domain.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IWorkSubmissionRepository : IGenericRepository<WorkSubmission>
    {
        Task<PagedList<WorkSubmission>> GetPagedAsync(WorkSubmissionQueryParams queryParams, CancellationToken cancellationToken = default);
        Task<WorkSubmissionDetail?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateTaskNameAsync(Guid taskId, string newTaskName, CancellationToken cancellationToken = default);
    }
}
