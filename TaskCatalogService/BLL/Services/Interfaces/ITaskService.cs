using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Task;
using DAL.Pagination;
using Domain.Query;

namespace BLL.Services.Interfaces
{
    public interface ITaskService
    {
        Task<PagedList<TaskReadDto>> GetPagedTasksAsync(TaskQueryParameters parameters, CancellationToken cancellationToken = default);
        Task<TaskDetailDto?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TaskDetailDto> CreateTaskAsync(TaskCreateDto dto, CancellationToken cancellationToken = default);
        Task<TaskDetailDto> UpdateTaskAsync(Guid id, TaskUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
