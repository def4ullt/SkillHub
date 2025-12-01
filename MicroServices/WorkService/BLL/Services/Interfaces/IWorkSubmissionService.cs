using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmission;
using DAL.Helpers;

namespace BLL.Services.Interfaces
{
    public interface IWorkSubmissionService
    {
        Task<WorkSubmissionReadDto> CreateAsync(WorkSubmissionCreateDto dto, CancellationToken cancellationToken = default);
        Task<WorkSubmissionDetailDto?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<WorkSubmissionReadDto> UpdateAsync(Guid id, WorkSubmissionUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<WorkSubmissionReadDto>> GetPagedAsync(WorkSubmissionQueryParams queryParams, CancellationToken cancellationToken = default);
    }
}
