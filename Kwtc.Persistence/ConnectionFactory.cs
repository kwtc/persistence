namespace Kwtc.Persistence;

using System.Data;

public abstract class ConnectionFactory : IConnectionFactory
{
    public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
    {
        return await this.GetAsync("ConnectionStrings:DefaultConnection", cancellationToken);
    }

    public abstract Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default);
}
