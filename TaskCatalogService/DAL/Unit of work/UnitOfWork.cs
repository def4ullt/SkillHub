using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DB;
using DAL.Repositories.Interfaces;

namespace DAL.Unit_of_work
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatalogServiceDbContext context;

        public ITagRepository Tags { get; }
        public ITaskRepository Tasks { get; }
        public ITechnologyRepository Technologies { get; }

        public UnitOfWork(CatalogServiceDbContext context, ITagRepository tagRepository, ITaskRepository taskRepository, ITechnologyRepository technologyRepository)
        {
            this.context = context;
            Tags = tagRepository;
            Tasks = taskRepository;
            Technologies = technologyRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}