namespace Kwtc.Persistence;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class MsSqlSharedConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public MsSqlSharedConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));

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
