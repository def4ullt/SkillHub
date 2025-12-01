using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Tag;

namespace BLL.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TagReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TagReadDto> CreateAsync(TagCreateDto dto, CancellationToken cancellationToken = default);
        Task<TagReadDto> UpdateAsync(Guid id, TagUpdateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
