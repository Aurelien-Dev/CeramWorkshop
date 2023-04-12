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

        public virtual async Task<T> Get(TId id) => await Context.Set<T>().FindAsync(id);

        public virtual async Task<ICollection<T>> GetAll() => await Context.Set<T>().ToListAsync();

        public async Task Add(T entity) => await Context.Set<T>().AddAsync(entity);

        public void Update(T entity) => Context.Set<T>().Update(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);
    }
}