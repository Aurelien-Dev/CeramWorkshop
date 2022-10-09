using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWorker : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IFiringRepository FiringRepository { get; }
        int Completed();
        void Rollback();
    }
}
