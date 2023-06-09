namespace Kwtc.Persistence;

/// <summary>
/// Tagging interface for Sqlite shared connection factory.
/// The intention is that you should be able to register this interface with your DI container along with one of
/// the other connection factories, and use this implementation for testing.
/// </summary>
public interface ISqliteSharedConnectionFactory : IConnectionFactory
{
}
