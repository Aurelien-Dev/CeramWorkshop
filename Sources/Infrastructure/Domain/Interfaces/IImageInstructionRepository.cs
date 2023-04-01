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

        /// <summary>
        /// Retrieves the favorite ImageInstruction for a specified product, or the first ImageInstruction associated with the product if there is no favorite.
        /// </summary>
        /// <param name="idProduct">The ID of the product to retrieve the ImageInstruction for.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the requested ImageInstruction object, or null if none are found.</returns>
        Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct);
      
        /// <summary>
        /// Sets a new favorite image instruction for the specified product.
        /// </summary>
        /// <param name="isFavorite">Whether the image instruction should be set as the new favorite.</param>
        /// <param name="id">The ID of the image instruction to set as the new favorite.</param>
        /// <param name="idProduct">The ID of the product associated with the image instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SetNewFavorite(bool isFavorite, int id, int idProduct);
    }
}