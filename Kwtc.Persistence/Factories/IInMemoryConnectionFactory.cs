namespace Kwtc.Persistence.Factories;

using Microsoft.Data.Sqlite;

public interface IInMemoryConnectionFactory : IDisposable
{
    Task<SqliteConnection> GetAsync(CancellationToken cancellationToken = default);
}
