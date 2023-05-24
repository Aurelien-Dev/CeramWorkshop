namespace Domain.InterfacesWorker
{
    public interface IWorkerBase
    {
        Task<int> Completed(CancellationToken cancellationToken = default);
    }
}