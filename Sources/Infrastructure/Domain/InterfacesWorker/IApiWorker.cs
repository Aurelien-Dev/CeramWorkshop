using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IApiWorker : IDisposable, IWorkerBase
    {
        IImageInstructionRepository ImageInstructionRepository { get; }
    }
}
