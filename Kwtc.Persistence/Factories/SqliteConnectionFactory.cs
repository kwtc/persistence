namespace Kwtc.Persistence.Factories;

using System.Data;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

public class SqliteConnectionFactory : ConnectionFactory
{
    private readonly IConfiguration configuration;

    public SqliteConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public override async Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(configKey, nameof(configKey));

        var connection = new SqliteConnection(this.configuration.GetValue<string>(configKey));

        try
        {
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
        catch (Exception)
        {
            await connection.DisposeAsync();
            throw;
        }
    }
}
