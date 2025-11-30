using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Unit_of_work
{
    public interface IUnitOfWork : IDisposable
    {
        ITagRepository Tags { get; }
        ITaskRepository Tasks { get; }
        ITechnologyRepository Technologies { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
