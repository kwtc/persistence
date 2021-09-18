namespace Kwtc.Persistence
{
    using System.Data;

    public interface IDbInstance
    {
        IDbProvider DatabaseProvider { get; }

        IDbConnection Connection { get; }
    }
}
