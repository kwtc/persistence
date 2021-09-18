namespace Kwtc.Persistence
{
    using System.Data;

    public interface IDbProvider
    {
        IDbConnection CreateConnection();
    }
}
