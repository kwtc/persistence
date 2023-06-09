namespace Kwtc.Persistence;

using System.Data;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

public class SqliteSharedConnectionFactory : ISqliteSharedConnectionFactory, IDisposable
{
    private readonly IConfiguration configuration;
    private readonly SqliteConnection masterConnection;

    public SqliteSharedConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
        SQLitePCL.Batteries.Init();
        this.masterConnection = new SqliteConnection($"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared");
        this.masterConnection.Open();
    }

    public void Dispose()
    {
        this.masterConnection.Close();
        this.masterConnection.Dispose();
    }

    public async Task<IDbConnection> GetAsync(string connectionStringKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

        var connection = new SqliteConnection(this.configuration.GetSection("ConnectionStrings")[connectionStringKey]);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
