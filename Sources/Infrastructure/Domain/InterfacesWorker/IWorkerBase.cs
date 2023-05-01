namespace Domain.InterfacesWorker
{
    public interface IWorkerBase
    {
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> Completed(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back all pending changes in the database context.
        /// </summary>
        void Rollback();
    }
}