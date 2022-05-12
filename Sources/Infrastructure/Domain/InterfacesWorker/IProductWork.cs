using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        int Completed();
        void Rollback();
    }
}
