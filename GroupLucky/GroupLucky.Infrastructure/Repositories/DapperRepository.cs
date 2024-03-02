using GroupLucky.Application.Contracts.Persistences;
using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class DapperRepositoryBase : IDapperRepository
    {
        public IDbConnection Connection { get; private set; }

        public DapperRepositoryBase(IDbConnection connection)
        {
            Connection = connection;
        }
    }

}
