namespace Kwtc.Persistence.Database;

public interface IInMemoryDatabase : IDisposable
{
    string ConnectionString { get; }
}
