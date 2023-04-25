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

        public async Task<int> Completed()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Clear();
        }

        public virtual void Close()
        {
            throw new NotImplementedException();
        }
    }
}