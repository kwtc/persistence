using System.Data;

namespace Kwtc.Persistence;

public interface IConnectionFactory
{
    Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);
}
