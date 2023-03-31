using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class WorkerBase : IWorkerBase
    {
        public readonly ApplicationDbContext _context;

        public WorkerBase(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <returns>Number of changes</returns>
        public int Completed()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Undo changes
        /// </summary>
        public void Rollback()
        {
            _context.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}