namespace Kwtc.Persistence;

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class MySqlSharedConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public MySqlSharedConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IDbConnection> GetAsync(string connectionStringKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

        var connection = new MySqlConnection(this.configuration.GetSection("ConnectionStrings")[connectionStringKey]);

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
