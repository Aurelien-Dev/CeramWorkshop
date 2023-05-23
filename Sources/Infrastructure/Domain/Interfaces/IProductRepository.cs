using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        /// <summary>
        /// Retrieves a product with its associated relationships based on the provided product ID and workshop ID.
        /// </summary>
        /// <param name="id">The product ID to filter by.</param>
        /// <param name="idWorkshop">The workshop ID to filter by.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task representing the asynchronous operation, 
        ///     with a result containing the requested Product object.
        /// </returns>
        Task<Product> Get(int id, int idWorkshop, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all products with their associated ImageInstructions for a given workshop ID.
        /// </summary>
        /// <param name="idWorkshop">The workshop ID to filter products by.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a collection of Product objects with their associated ImageInstructions.</returns>
        Task<ICollection<Product>> GetAll(int idWorkshop, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product without associated relationships based on the provided product ID.
        /// </summary>
        /// <param name="id">The product ID to filter by.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns> A task representing the asynchronous operation with a result containing the requested Product object without associated details.</returns>
        Task<Product> GetLight(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the number of ImageInstruction records associated with the provided product ID.
        /// </summary>
        /// <param name="id">The product ID for which to count the ImageInstruction records.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the count of ImageInstruction records for the specified product.</returns>
        Task<int> CountImage(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the cost and quantity of an existing ProductMaterial in the database.
        /// </summary>
        /// <param name="productMaterial">The ProductMaterial object containing updated cost and quantity information.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <remarks>If the input ProductMaterial is null, the method will return without performing any updates.</remarks>
        Task UpdateProductMaterialCostAndQuantity(ProductMaterial productMaterial, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the CostKwH property of an existing ProductFiring object in the database.
        /// </summary>
        /// <param name="productFiring">The ProductFiring object with updated data to be saved.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <remarks>If the provided ProductFiring object is null, the method will not perform any action.</remarks>
        Task UpdateProductFiring(ProductFiring productFiring, CancellationToken cancellationToken = default);
        
        Task AddMaterial(ProductMaterial pMat, CancellationToken cancellationToken = default);
    }
}