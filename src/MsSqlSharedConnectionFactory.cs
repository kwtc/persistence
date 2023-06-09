namespace Kwtc.Persistence;

using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class MsSqlSharedConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public MsSqlSharedConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
