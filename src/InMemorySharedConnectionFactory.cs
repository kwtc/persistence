namespace Kwtc.Persistence;

using System.Data;
using Microsoft.Data.Sqlite;

public class InMemorySharedConnectionFactory : IInMemoryConnectionFactory, IDisposable
{
    private readonly SqliteConnection connection;
    private readonly string connectionString;

    public InMemorySharedConnectionFactory()
    {
        this.connectionString = $"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared";
        SQLitePCL.Batteries.Init();
        this.connection = new SqliteConnection(this.connectionString);

        this.connection.Open();
    }

    public void Dispose()
    {
        this.connection.Close();
        this.connection.Dispose();
    }

    public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqliteConnection(this.connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
