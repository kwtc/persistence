using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Kwtc.Persistence
{
    public class MySqlSharedConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration configuration;

        public MySqlSharedConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default)
        {
            var connection = new MySqlConnection(this.configuration.GetConnectionString("ConnectionString"));

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
}
