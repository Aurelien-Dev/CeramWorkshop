using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetLight(object id);
        Task<int> CountImage(int id);
        void UpdateProductMaterial(ProductMaterial productMaterial);
    }
}