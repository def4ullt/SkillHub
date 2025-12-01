using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Technology;

namespace BLL.Services.Interfaces
{
    public interface ITechnologyService
    {
        Task<List<TechnologyReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TechnologyReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TechnologyReadDto> CreateAsync(TechnologyCreateDto dto, CancellationToken cancellationToken = default);
        Task<TechnologyReadDto> UpdateAsync(Guid id, TechnologyUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
