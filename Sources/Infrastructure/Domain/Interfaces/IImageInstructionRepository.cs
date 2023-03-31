using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IImageInstructionRepository : IGenericRepository<ImageInstruction, int>
    {
        /// <summary>
        /// Retrieves all ImageInstruction objects that have not been exported, i.e., their FileLocation is set to 'Server'.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result containing an IEnumerable of ImageInstruction objects with a FileLocation set to 'Server'.</returns>
        Task<IEnumerable<ImageInstruction>> GetAllNonExported();
    }
}