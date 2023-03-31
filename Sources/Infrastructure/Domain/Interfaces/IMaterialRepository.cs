using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IMaterialRepository : IGenericRepository<Material, int>
    {
        /// <summary>
        /// Retrieves a collection of materials filtered by the specified material type.
        /// </summary>
        /// <param name="type">The MaterialType to filter the materials by.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the collection of Material objects matching the specified type.</returns>
        Task<ICollection<Material>> GetAll(MaterialType type);

        /// <summary>
        /// Updates the cost of all product materials associated with a specific material ID.
        /// </summary>
        /// <param name="idMat">The material ID to filter by and update associated product material costs.</param>
        void UpdateAllMaterialCost(int idMat);
    }
}