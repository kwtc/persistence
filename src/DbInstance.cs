namespace Kwtc.Persistence
{
    using System.Data;

    public class DbInstance : IDbInstance
    {
        public DbInstance(IDbProvider databaseProvider)
        {
            this.DatabaseProvider = databaseProvider;
        }

        public IDbProvider DatabaseProvider { get; }

        public IDbConnection Connection => this.DatabaseProvider.CreateConnection();
    }
}
