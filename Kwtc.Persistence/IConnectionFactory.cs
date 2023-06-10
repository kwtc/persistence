using System.Data;

namespace Kwtc.Persistence;

public interface IConnectionFactory
{
    /// <summary>
    /// Gets a connection from the default connection string ConnectionStrings:DefaultConnection.
    /// </summary>
    Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a connection from the connection string with the given key.
    /// </summary>
    Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default);
}
