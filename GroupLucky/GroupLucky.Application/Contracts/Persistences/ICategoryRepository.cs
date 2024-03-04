using GroupLucky.Domain;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface ICategoryRepository
    {
        Task<int> Add(Category entity);
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategory(int id);
        Task Update(Category entity);
    }
}
