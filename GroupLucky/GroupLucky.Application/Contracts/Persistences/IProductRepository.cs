using GroupLucky.Application.Features.Products.Queries;
using GroupLucky.Domain;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProduct(int id);
        Task<int> Add(Product entity);
        Task Update(Product entity);
        Task Delete(int id);
    }
}
