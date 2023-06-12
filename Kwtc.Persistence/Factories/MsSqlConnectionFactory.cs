namespace Kwtc.Persistence.Factories;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class MsSqlConnectionFactory : ConnectionFactory
{
    private readonly IConfiguration configuration;

    public MsSqlConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public override async Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(configKey, nameof(configKey));

        var connection = new SqlConnection(this.configuration.GetValue<string>(configKey));

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
