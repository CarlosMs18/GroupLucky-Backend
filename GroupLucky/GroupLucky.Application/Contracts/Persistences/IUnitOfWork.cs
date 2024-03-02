namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        void Commit();
    }
}
