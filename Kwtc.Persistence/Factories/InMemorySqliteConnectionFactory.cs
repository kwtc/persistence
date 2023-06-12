namespace Kwtc.Persistence.Factories;

using Microsoft.Data.Sqlite;

public class InMemorySqliteConnectionFactory : IInMemoryConnectionFactory
{
    private readonly IInMemoryDatabase database;
    private readonly string connectionString;

    public InMemorySqliteConnectionFactory()
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
