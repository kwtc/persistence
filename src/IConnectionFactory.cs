using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Kwtc.Persistence
{
    public interface IConnectionFactory
    {
        Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);
    }
}
