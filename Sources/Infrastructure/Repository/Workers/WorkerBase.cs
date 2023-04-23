using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class WorkerBase : IWorkerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor of base worker
        /// </summary>
        /// <param name="context">Db Context</param>
        protected WorkerBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Completed()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Clear();
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
                _context.Dispose();
            }
        }
    }
}