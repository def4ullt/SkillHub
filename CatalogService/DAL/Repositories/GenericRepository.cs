using DAL.DB;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected CatalogServiceDbContext context;
        protected DbSet<TEntity> dbSet;
        private ISpecificationEvaluator specificationEvaluator;

        public GenericRepository(CatalogServiceDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            specificationEvaluator = SpecificationEvaluator.Default;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return specificationEvaluator.GetQuery(dbSet.AsQueryable(), spec);
        }
    }
}