using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IWorkshopWorker : IDisposable
    {
        IWorkshopRepository WorkshopRepository { get; }
        int Completed();
        void Rollback();
    }
}
