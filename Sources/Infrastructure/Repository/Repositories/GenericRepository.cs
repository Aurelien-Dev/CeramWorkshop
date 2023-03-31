using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Get(TId id) => await _context.Set<T>().FindAsync(id);

        public virtual async Task<ICollection<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task Add(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}