namespace Kwtc.Persistence
{
    using System.Data;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;

    public class MySqlDbProvider : IDbProvider
    {
        private readonly IConfiguration configuration;

        public MySqlDbProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(this.configuration.GetConnectionString("ConnectionString"));
        }
    }
}
