using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<bool> AreTagsValidAsync(IEnumerable<Guid> tagIds, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    }
}
