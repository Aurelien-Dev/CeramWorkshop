namespace Domain.InterfacesWorker
{
    public interface IWorkerBase : IDisposable
    {
        int Completed();
        void Rollback();
    }
}
