namespace Kwtc.Persistence;

using System.Data;

public interface IInMemoryConnectionFactory : IDisposable
{
    Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);
}
