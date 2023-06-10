namespace Kwtc.Persistence;

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class MySqlConnectionFactory : ConnectionFactory
{
    private readonly IConfiguration configuration;

    public MySqlConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public override async Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(configKey, nameof(configKey));

        var connection = new MySqlConnection(this.configuration.GetValue<string>(configKey));

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
