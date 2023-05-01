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

        public async Task Add(T entity, CancellationToken cancellationToken = default) => await Context.Set<T>().AddAsync(entity, cancellationToken);

        public void Update(T entity) => Context.Set<T>().Update(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);
    }
}