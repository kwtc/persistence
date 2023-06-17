namespace Kwtc.Persistence.Extensions;

using System.Data;
using System.Text;
using CommunityToolkit.Diagnostics;
using Dapper;
using Microsoft.Data.Sqlite;

public static class SqliteConnectionExtensions
{
    /// <summary>
    /// Creates a table if it does not exist.
    /// Will attempt to map the properties of the type to the appropriate SQL data type.
    /// If unable to find a mapping, the property will be ignored.
    /// </summary>
    public static void CreateTable<T>(this SqliteConnection connection, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        connection.Execute(CreateTableScript(typeof(T), tableName));
    }

    /// <summary>
    /// Creates a table asynchronously if it does not exist.
    /// Will attempt to map the properties of the type to the appropriate SQL data type.
    /// If unable to find a mapping, the property will be ignored.
    /// </summary>
    public static async Task CreateTableAsync<T>(this SqliteConnection connection, string tableName, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        await connection.ExecuteAsync(new CommandDefinition(CreateTableScript(typeof(T), tableName), cancellationToken: cancellationToken));
    }

    /// <summary>
    /// Drops a table if it exists.
    /// </summary>
    public static void DropTable(this SqliteConnection connection, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        connection.Execute($"DROP TABLE IF EXISTS {tableName}");
    }

    /// <summary>
    /// Drops a table asynchronously if it exists.
    /// </summary>
    public static async Task DropTableAsync(this SqliteConnection connection, string tableName, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        await connection.ExecuteAsync(new CommandDefinition($"DROP TABLE IF EXISTS {tableName}", cancellationToken: cancellationToken));
    }

    private static string CreateTableScript(Type type, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        var props = type.GetProperties().Select(p => new KeyValuePair<string, Type>(p.Name, p.PropertyType)).ToList();

        var script = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName}(");
        foreach (var prop in props)
        {
            var propType = GetMappedType(prop.Value);
            script.Append("\"" + prop.Key + "\" " + propType);

            if (prop.Key != props.Last().Key)
            {
                script.Append(',');
            }
        }

        script.Append(')');

        return script.ToString();
    }

    private static string GetMappedType(Type type)
    {
        switch (type)
        {
            case not null when type == typeof(bool):
            case not null when type == typeof(byte):
            case not null when type == typeof(short):
            case not null when type == typeof(int):
            case not null when type == typeof(long):
            case not null when type == typeof(sbyte):
            case not null when type == typeof(ushort):
            case not null when type == typeof(uint):
            case not null when type == typeof(ulong):
                return "INTEGER";
            case not null when type == typeof(char):
            case not null when type == typeof(DateOnly):
            case not null when type == typeof(DateTime):
            case not null when type == typeof(DateTimeOffset):
            case not null when type == typeof(decimal):
            case not null when type == typeof(Guid):
            case not null when type == typeof(string):
            case not null when type == typeof(TimeOnly):
            case not null when type == typeof(TimeSpan):
                return "TEXT";
            case not null when type == typeof(double):
            case not null when type == typeof(float):
                return "REAL";
            case not null when type == typeof(byte[]):
                return "BLOB";
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
