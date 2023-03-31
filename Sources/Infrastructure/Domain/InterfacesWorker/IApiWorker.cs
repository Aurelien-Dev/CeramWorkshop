using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IApiWorker : IWorkerBase
    {
        IImageInstructionRepository ImageInstructionRepository { get; }
    }
}
