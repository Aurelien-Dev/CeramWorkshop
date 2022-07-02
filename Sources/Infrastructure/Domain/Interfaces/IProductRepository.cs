using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetLight(object id);
        Task<int> CountImage(int id);
        Task UpdateProductMaterial(ProductMaterial productMaterial);
    }
}