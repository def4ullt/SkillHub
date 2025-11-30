using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.WorkSubmissionStatus;

namespace BLL.Services.Interfaces
{
    public interface IWorkSubmissionStatusService
    {
        Task<IEnumerable<WorkSubmissionStatusReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<WorkSubmissionStatusReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<WorkSubmissionStatusReadDto> CreateAsync(WorkSubmissionStatusCreateDto dto, CancellationToken cancellationToken = default);
        Task<WorkSubmissionStatusReadDto> UpdateAsync(Guid id, WorkSubmissionStatusUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
