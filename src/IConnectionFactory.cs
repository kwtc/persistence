using System.Data;

namespace Kwtc.Persistence;

public interface IConnectionFactory
{
    Task<IDbConnection> GetAsync(string connectionStringKey, CancellationToken cancellationToken = default);
}
