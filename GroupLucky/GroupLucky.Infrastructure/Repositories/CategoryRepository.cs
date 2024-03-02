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

        public void Add(Category entity)
        {
            throw new NotImplementedException();
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
            var categories = await Connection.QueryAsync<Category>("SELECT * FROM Category", transaction: Transaction);
            return categories.ToList();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
