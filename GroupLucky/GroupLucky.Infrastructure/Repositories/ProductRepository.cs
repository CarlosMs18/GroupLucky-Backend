﻿using Dapper;
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
            const string sql = @"INSERT INTO Products (Code,Name, StockMin,StockMax,UnitSalePrice,CategoryId, Active) 
                                 VALUES (@Code,@Name, @StockMin, @StockMax, @UnitSalePrice, @CategoryId, @Active);
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

        public async Task Delete(int id)
        {
            const string sql = @"UPDATE Products 
                                SET Active = 0
                                WHERE Id = @Id";


            await Connection.ExecuteAsync(sql, new {Id = id}, Transaction);
        }
    }
}
