using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public interface IProductWorker : IWorkerBase
    {
        IProductRepository ProductRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IFiringRepository FiringRepository { get; }
        IImageInstructionRepository ImageInstructionRepository { get; }
        void Close();
    }
}
