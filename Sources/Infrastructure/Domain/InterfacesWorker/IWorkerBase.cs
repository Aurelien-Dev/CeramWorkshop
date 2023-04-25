﻿namespace Domain.InterfacesWorker
{
    public interface IWorkerBase
    {
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> Completed();

        /// <summary>
        /// Rolls back all pending changes in the database context.
        /// </summary>
        void Rollback();

        void Close();
    }
}