namespace Kwtc.Persistence
{
    using System.Data;
    using Microsoft.Data.SqlClient;
#if NETSTANDARD
    using Microsoft.Data.SqlClient;
#else
    using System.Data.SqlClient;
#endif
    using Microsoft.Extensions.Configuration;

    public class MsSqlDbProvider : IDbProvider
    {
        private readonly IConfiguration configuration;

        public MsSqlDbProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
        }
    }
}
