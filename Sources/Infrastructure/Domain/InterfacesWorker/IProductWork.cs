using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IFiringRepository FiringRepository { get; }
        IAccessoryRepository AccessoryRepository { get; }
        int Completed();
        void Rollback();
    }
}
