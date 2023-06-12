namespace Kwtc.Persistence.Factories;

using Database;
using Microsoft.Data.Sqlite;

public class InMemoryConnectionFactory : IInMemoryConnectionFactory
{
    private readonly IInMemoryDatabase database;
    private readonly string connectionString;

    public InMemoryConnectionFactory()
    {
        this.database = new InMemoryDatabase();
        this.connectionString = database.ConnectionString;
    }

    public async Task<SqliteConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqliteConnection(this.connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

    public void Dispose()
    {
        this.database.Dispose();
    }
}
