using Dapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Domain;
using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        public async Task<IEnumerable<Product>> GetAll()
        {

            var query = @"
                SELECT p.Id, p.Code, p.Name, p.StockMax, p.StockMin, p.UnitSalePrice, p.Active, p.CategoryId,
                       c.Id , c.Name 
                FROM Products p
                LEFT JOIN Categories c ON c.Id = p.CategoryId
                WHERE p.Active = 1";

            var products = await Connection.QueryAsync<Product, Category, Product>(
                query,
                (product, category) =>
                {
                    if (category != null)
                    {
                        product.CategoryId = category.Id;
                        product.Category = category;
                    }
                    return product;
                },
                splitOn: "CategoryId",
                transaction: Transaction);

            return products;
        }



        public async Task<Product> GetProduct(int id)
        {
            var parameters = new { ProductId = id };
            var product = await Connection.QueryFirstOrDefaultAsync<Product>(
                "Get_Product",
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);

            return product;
        }
        public async Task<int> Add(Product entity)
        {
            var parameters = new
            {
                entity.Code,
                entity.Name,
                entity.StockMin,
                entity.StockMax,
                entity.UnitSalePrice,
                entity.CategoryId,
                entity.Active
            };

            const string storedProcedureName = "Add_Product";

            return await Connection.ExecuteScalarAsync<int>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);
        }


        public async Task Update(Product entity)
        {
            var parameters = new
            {
                entity.Id,
                entity.Code,
                entity.Name,
                entity.StockMin,
                entity.StockMax,
                entity.UnitSalePrice,
                entity.CategoryId
            };

            const string storedProcedureName = "Update_Product";

            await Connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);
        }


        public async Task Delete(int id)
        {
            const string storedProcedureName = "Delete_Product";

            await Connection.ExecuteAsync(
                storedProcedureName,
                new { Id = id },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction);
        }

    }
}
