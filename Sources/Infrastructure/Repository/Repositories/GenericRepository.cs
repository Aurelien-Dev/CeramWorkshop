using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        protected readonly ApplicationDbContext Context;
        private CancellationTokenSource? _cancellationTokenSource;

        protected CancellationToken ComponentDisposed => (_cancellationTokenSource ??= new()).Token;

        protected GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public virtual async Task<T> Get(TId id) => await Context.Set<T>().FindAsync(id, ComponentDisposed);

        public virtual async Task<ICollection<T>> GetAll() => await Context.Set<T>().ToListAsync(ComponentDisposed);

        public async Task Add(T entity) => await Context.Set<T>().AddAsync(entity, ComponentDisposed);

        public void Update(T entity) => Context.Set<T>().Update(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);

        
        public void CancelEFCore()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}