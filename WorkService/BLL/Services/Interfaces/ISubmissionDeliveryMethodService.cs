using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.DeliveryMethod;

namespace BLL.Services.Interfaces
{
    public interface ISubmissionDeliveryMethodService
    {
        Task<IEnumerable<SubmissionDeliveryMethodReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SubmissionDeliveryMethodReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SubmissionDeliveryMethodReadDto> CreateAsync(SubmissionDeliveryMethodCreateDto dto, CancellationToken cancellationToken = default);
        Task<SubmissionDeliveryMethodReadDto> UpdateAsync(Guid id, SubmissionDeliveryMethodUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
