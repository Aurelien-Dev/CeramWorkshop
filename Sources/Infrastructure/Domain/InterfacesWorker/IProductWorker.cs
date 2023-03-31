using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWorker : IDisposable, IWorkerBase
    {
        IProductRepository ProductRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IFiringRepository FiringRepository { get; }
    }
}
