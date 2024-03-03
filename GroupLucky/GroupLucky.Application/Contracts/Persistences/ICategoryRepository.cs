using GroupLucky.Domain;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface ICategoryRepository
    {
        Task<int> Add(Category entity);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategory(int id);
        void Delete(int id);
        void Delete(Product entity);
        Category Find(int id);
        Category FindByName(string name);
        void Update(Category entity);
    }
}
