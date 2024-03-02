using System.Data;

namespace GroupLucky.Application.Contracts.Persistences
{
    public interface IDapperRepository
    {
        IDbConnection Connection { get; }
    }
}
