using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class RepositoryBase
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null");
            //Transaction = transaction;
        }
    }
}
