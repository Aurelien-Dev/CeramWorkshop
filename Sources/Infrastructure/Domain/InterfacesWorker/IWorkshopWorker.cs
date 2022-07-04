using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IWorkshopWorker
    {
        IWorkshopRepository WorkshopRepository { get; }
        int Completed();
        void Rollback();
    }
}
