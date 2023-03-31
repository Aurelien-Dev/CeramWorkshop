namespace Domain.Interfaces
{
    public interface IGenericRepository<T, TId> where T : class
    {
        /// <summary>
        /// Retrieves an entity of type T by its primary key (ID).
        /// </summary>
        /// <param name="id">The primary key (ID) of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the requested entity of type T or null if not found.</returns>
        Task<T> Get(TId id);

        /// <summary>
        /// Retrieves all entities of type T from the database.
        /// </summary>
        /// <returns>A collection of entities of type T.</returns>
        Task<ICollection<T>> GetAll();

        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the underlying data store asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add to the data store.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Add(T entity);

        /// <summary>
        /// Updates the specified entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity from the context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);
    }
}