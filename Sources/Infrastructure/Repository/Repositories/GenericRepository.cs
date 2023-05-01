using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        protected readonly ApplicationDbContext Context;

        protected GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public virtual async Task<T> Get(TId id, CancellationToken cancellationToken = default) => await Context.Set<T>().FindAsync(id, cancellationToken);

        public virtual async Task<ICollection<T>> GetAll(CancellationToken cancellationToken = default) => await Context.Set<T>().ToListAsync(cancellationToken);

        public async Task Add(T entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<T>().AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}