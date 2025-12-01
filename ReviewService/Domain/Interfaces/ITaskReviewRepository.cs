using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.QueryParams;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface ITaskReviewRepository : IGenericRepository<TaskReview>
    {
        Task<bool> HasUserReviewedTaskAsync(Guid userId, Guid taskId, CancellationToken cancellationToken = default);
        Task<PagedList<TaskReview>> GetReviewsAsync(TaskReviewQueryParameters queryParameters, CancellationToken cancellationToken = default);
        Task<bool> TaskIdExistsAsync(Guid taskId, CancellationToken cancellationToken = default);
    }
}
