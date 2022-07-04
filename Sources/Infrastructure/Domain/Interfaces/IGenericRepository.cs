namespace Domain.Interfaces
{
    public interface IGenericRepository<T, TId> where T : class
    {
        Task<T> Get(TId id);
        Task<ICollection<T>> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}