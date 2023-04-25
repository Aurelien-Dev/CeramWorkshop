using Domain.Interfaces;
using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class WorkshopWorker : WorkerBase, IWorkshopWorker
    {
        public IWorkshopRepository WorkshopRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="workshopRepository">Workshop Repository</param>
        public WorkshopWorker(ApplicationDbContext context, IWorkshopRepository workshopRepository)
            : base(context)
        {
            WorkshopRepository = workshopRepository;
        }

        public override void Close()
        {
            WorkshopRepository.CancelEFCore();
        }
    }
}