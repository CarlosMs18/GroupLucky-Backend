﻿using Dapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Domain;
using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        public CategoryRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<int> Add(Category entity)
        {
            const string sql = @"INSERT INTO Categories (Name, Description) VALUES (@Name, @Description);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

            return await Connection.ExecuteScalarAsync<int>(sql, entity, Transaction);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Category Find(int id)
        {
            throw new NotImplementedException();
        }

        public Category FindByName(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await Connection.QueryAsync<Category>(
                @"SELECT * FROM Categories", transaction: Transaction);
            return categories.ToList();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await Connection.QueryFirstOrDefaultAsync<Category>(
                @"SELECT Id, Name, Description
                FROM Categories 
                WHERE Id = @Id",
                new { id },
                transaction: Transaction);

            return category!;
        }
    

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
