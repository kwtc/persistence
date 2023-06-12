namespace Kwtc.Persistence.Database;

using Microsoft.Data.Sqlite;

public class InMemoryDatabase : IInMemoryDatabase
{
    public string ConnectionString { get; }
    private readonly SqliteConnection connection;

    public InMemoryDatabase()
    {
        this.ConnectionString = $"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared";

        SQLitePCL.Batteries.Init();

        this.connection = new SqliteConnection(this.ConnectionString);
        this.connection.Open();
    }

    public void Dispose()
    {
        this.connection.Close();
        this.connection.Dispose();
    }
}
