using GroupLucky.Domain;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface ICategoryRepository
    {
        void Add(Category entity);
        Task<IEnumerable<Category>> GetAll();
        void Delete(int id);
        void Delete(Product entity);
        Category Find(int id);
        Category FindByName(string name);
        void Update(Category entity);
    }
}
