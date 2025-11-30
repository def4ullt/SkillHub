using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IWorkSubmissionStatusRepository : IGenericRepository<WorkSubmissionStatus>
    {
        Task<bool> IsNameUniqueAsync(string name, Guid? idToExclude = null, CancellationToken cancellationToken = default);
    }
}
