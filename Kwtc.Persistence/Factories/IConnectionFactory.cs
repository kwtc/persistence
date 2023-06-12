namespace Kwtc.Persistence.Factories;

using System.Data;

public interface IConnectionFactory
{
    /// <summary>
    /// Gets a <see cref="IDbConnection"/> using the default connection string ConnectionStrings:DefaultConnection.
    /// </summary>
    Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a <see cref="IDbConnection"/> using the connection string with the given key.
    /// </summary>
    Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default);
}
