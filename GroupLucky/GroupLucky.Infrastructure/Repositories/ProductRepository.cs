﻿using Dapper;
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

        public async Task<int> Add(Product entity)
        {
            const string sql = @"INSERT INTO Products (Code,Name, StockMin,StockMax,UnitSalePrice,CategoryId) 
                                 VALUES (@Code,@Name, @StockMin, @StockMax, @UnitSalePrice, @CategoryId);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            return await Connection.ExecuteScalarAsync<int>(sql, entity, Transaction);
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

        public async Task<Product> GetProduct(int id)
        {
            var product = await Connection.QueryFirstOrDefaultAsync<Product>(
                @"SELECT Id, Code, Name, StockMin, StockMax, UnitSalePrice
                FROM Products 
                WHERE Id = @Id",
                new {id}, 
                transaction:Transaction);

            return product;
        }
    }
}
