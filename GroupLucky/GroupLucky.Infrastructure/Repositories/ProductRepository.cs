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
              @"SELECT p.Id, p.Code, p.Name As ProductName, p.StockMax, p.StockMin, p.UnitSalePrice, c.Name As CategoryName, p.Active
              FROM Products p
              JOIN Categories c
              ON c.Id = p.CategoryId
              WHERE p.Active = 1", transaction: Transaction);
            return products.ToList();   
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await Connection.QueryFirstOrDefaultAsync<Product>(
                @"SELECT Id, Code, Name, StockMin, StockMax, UnitSalePrice, Active
                FROM Products 
                WHERE Id = @Id AND Active=1",
                new {id}, 
                transaction:Transaction);

            return product;
        }
        public async Task<int> Add(Product entity)
        {
            const string sql = @"INSERT INTO Products (Code,Name, StockMin,StockMax,UnitSalePrice,CategoryId) 
                                 VALUES (@Code,@Name, @StockMin, @StockMax, @UnitSalePrice, @CategoryId);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            return await Connection.ExecuteScalarAsync<int>(sql, entity, Transaction);
        }

        public async Task Update(Product entity)
        {
            await Connection.ExecuteAsync(@"UPDATE Products SET
                              Code = @Code,
                              Name = @Name,
                              StockMin = @StockMin,
                              StockMax = @StockMax,
                              UnitSalePrice = @UnitSalePrice,
                              CategoryId = @CategoryId
                              WHERE Id = @Id",
                                       new
                                       {
                                           entity.Code,
                                           entity.Name,
                                           entity.StockMin,
                                           entity.StockMax,
                                           entity.UnitSalePrice,
                                           entity.CategoryId,
                                           entity.Id
                                       },
                                       Transaction);
        }
    }
}
