using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class WorkerBase : IWorkerBase
    {
        public readonly ApplicationDbContext Context;

        /// <summary>
        /// Constructor of base worker
        /// </summary>
        /// <param name="context">Db Context</param>
        public WorkerBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public int Completed()
        {
            return Context.SaveChanges();
        }

        public void Rollback()
        {
            Context.ChangeTracker.Clear();
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the object, including the database context,
        /// if the object is being disposed.
        /// </summary>
        /// <param name="disposing">
        /// True if the object is being disposed; otherwise, false.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
    }
}