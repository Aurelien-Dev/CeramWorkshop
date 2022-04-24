using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        int CountImage(object id);
    }
}