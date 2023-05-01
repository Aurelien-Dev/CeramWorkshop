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

        public async Task<int> Completed(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Rollback()
        {
            _context.ChangeTracker.Clear();
        }
    }
}