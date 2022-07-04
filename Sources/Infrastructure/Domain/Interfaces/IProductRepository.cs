using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product, int>
    {
        Task<Product> Get(int id, int idWorkshop);
        Task<ICollection<Product>> GetAll(int idWorkshop);

        Task<Product> GetLight(object id);
        Task<int> CountImage(int id);
        void UpdateProductMaterial(ProductMaterial productMaterial);
    }
}