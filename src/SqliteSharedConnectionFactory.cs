namespace Kwtc.Persistence;

using System.Data;
using Microsoft.Data.Sqlite;

public class SqliteSharedConnectionFactory : ISqliteSharedConnectionFactory, IDisposable
{
    private readonly SqliteConnection masterConnection;
    private readonly string connectionString;

    public SqliteSharedConnectionFactory()
    {
        this.connectionString = $"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared";
        SQLitePCL.Batteries.Init();
        this.masterConnection = new SqliteConnection(this.connectionString);
        this.masterConnection.Open();
    }

    public void Dispose()
    {
        this.masterConnection.Close();
        this.masterConnection.Dispose();
    }

    public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqliteConnection(this.connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
