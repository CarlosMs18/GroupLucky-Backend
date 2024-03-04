using Dapper;
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

        public async Task Update(Category entity)
        {
            await Connection.ExecuteAsync(@"UPDATE Categories SET 
                                          Name = @Name, 
                                          Description = @Description
                                          WHERE Id = @Id",
                                          new
                                          {
                                              entity.Name,
                                              entity.Description,
                                              entity.Id
                                          },
                                          transaction: Transaction);
        }
    }
}
