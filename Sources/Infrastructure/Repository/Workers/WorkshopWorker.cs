using Domain.Interfaces;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;

namespace Repository.Workers
{
    public class WorkshopWorker : IWorkshopWorker
    {
        private readonly ApplicationDbContext _context;
        public IWorkshopRepository WorkshopRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="WorkshopRepository">Workshop Repository</param>
        public WorkshopWorker(ApplicationDbContext context, IWorkshopRepository workshopRepository)
        {
            _context = context;
            WorkshopRepository = workshopRepository;
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}