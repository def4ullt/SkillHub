using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using Domain.Entities;

namespace DAL.Unit_of_work
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkSubmissionRepository WorkSubmissions { get; }
        IWorkSubmissionFileRepository WorkSubmissionFiles { get; }
        IWorkSubmissionStatusRepository WorkSubmissionStatuses { get; }
        ISubmissionDeliveryMethodRepository SubmissionDeliveryMethods { get; }
        IDbConnection Connection { get; }
        IDbTransaction? Transaction { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
