namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        void Commit();
    }
}
