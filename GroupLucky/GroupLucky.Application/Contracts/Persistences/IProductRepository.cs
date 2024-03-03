using GroupLucky.Application.Features.Products.Queries;
using GroupLucky.Domain;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductQueryResponse>> GetAll();
        Task<Product> GetProduct(int id);
        Task<int> Add(Product entity);
    }
}
