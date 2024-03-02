namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        Task<int> ExecStoreProcedure(string sql, params object[] parameters);
        Task Rollback();
        Task Commit();
        Task BeginTransaction();
    }
}
