namespace Kwtc.Persistence;

public interface IInMemoryDatabase : IDisposable
{
    string ConnectionString { get; }
}
