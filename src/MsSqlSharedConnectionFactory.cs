using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Kwtc.Persistence
{
    public class MsSqlSharedConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration configuration;

        public MsSqlSharedConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
