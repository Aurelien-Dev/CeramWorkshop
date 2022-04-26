using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<int> CountImage(int id);
    }
}