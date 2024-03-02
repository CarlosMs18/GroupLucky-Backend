using Dapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Features.Products.Queries;
using GroupLucky.Domain;
using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<IEnumerable<GetProductQueryResponse>> GetAll()
        {
             var products = await Connection.QueryAsync<GetProductQueryResponse>(
              @"SELECT p.Id, p.Code, p.Name As ProductName, p.StockMax, p.StockMin, p.UnitSalePrice, c.Name As CategoryName
              FROM Products p
              JOIN Categories c
              ON c.Id = p.CategoryId", transaction: Transaction);
            return products.ToList();   
        }
    }
}
