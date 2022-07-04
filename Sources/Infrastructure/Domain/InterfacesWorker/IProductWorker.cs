using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWorker : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        int Completed();
        void Rollback();
    }
}
