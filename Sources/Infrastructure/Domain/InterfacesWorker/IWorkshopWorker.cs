using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IWorkshopWorker : IWorkerBase
    {
        IWorkshopRepository WorkshopRepository { get; }
    }
}