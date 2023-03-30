using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IImageInstructionRepository : IGenericRepository<ImageInstruction, int>
    {
        Task<IEnumerable<ImageInstruction>> GetAllNonExported();
    }
}