using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IWorkSubmissionFileRepository : IGenericRepository<WorkSubmissionFile>
    {
        Task<List<WorkSubmissionFile>> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    }
}
