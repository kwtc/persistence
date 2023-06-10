namespace Kwtc.Persistence;

using System.Data;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

public class SqliteConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public SqliteConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IDbConnection> GetAsync(string connectionStringKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

        var connection = new SqliteConnection(this.configuration.GetSection("ConnectionStrings")[connectionStringKey]);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
