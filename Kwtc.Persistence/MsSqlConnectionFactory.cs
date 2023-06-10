namespace Kwtc.Persistence;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class MsSqlConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public MsSqlConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IDbConnection> GetAsync(string connectionStringKey, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(connectionStringKey, nameof(connectionStringKey));

        var connection = new SqlConnection(this.configuration.GetSection("ConnectionStrings")[connectionStringKey]);

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
