namespace Domain.Interfaces
{
    public interface IGenericRepository<T, TId> where T : class
    {
        /// <summary>
        /// Retrieves an entity of type T by its primary key (ID).
        /// </summary>
        /// <param name="id">The primary key (ID) of the entity to retrieve.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the requested entity of type T or null if not found.</returns>
        Task<T> Get(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all entities of type T from the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A collection of entities of type T.</returns>
        Task<ICollection<T>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the underlying data store asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add to the data store.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Add(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task Update(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the specified entity from the context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task Delete(T entity, CancellationToken cancellationToken = default);

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}