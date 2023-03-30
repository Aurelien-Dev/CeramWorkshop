using Domain.Interfaces;

namespace Domain.InterfacesWorker
{
    public  interface IApiWorker : IDisposable
    {
        IImageInstructionRepository ImageInstructionRepository { get; }
        int Completed();
        void Rollback();
    }
}
