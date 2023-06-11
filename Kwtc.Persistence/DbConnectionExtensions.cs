namespace Kwtc.Persistence;

using System.Data;
using System.Text;
using CommunityToolkit.Diagnostics;
using Dapper;

public static class DbConnectionExtensions
{
    /// <summary>
    /// Creates a table if it does not exist.
    /// Will attempt to map the properties of the type to the appropriate SQL data type.
    /// If unable to find a mapping, the property will be ignored.
    /// </summary>
    public static void CreateTable<T>(this IDbConnection connection, string tableName)
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
    public static async Task CreateTableAsync<T>(this IDbConnection connection, string tableName, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        await connection.ExecuteAsync(CreateTableScript(typeof(T), tableName));
    }

    /// <summary>
    /// Drops a table if it exists.
    /// </summary>
    public static void DropTable(this IDbConnection connection, string tableName)
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
    public static void DropTableAsync(this IDbConnection connection, string tableName, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        connection.ExecuteAsync($"DROP TABLE IF EXISTS {tableName}");
    }

    private static string CreateTableScript(Type type, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        var props = type.GetProperties().Select(p => new KeyValuePair<string, Type>(p.Name, p.PropertyType)).ToList();

        var script = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName}(");
        foreach (var prop in props)
        {
            if (!DataTypeMapper.TryGetValue(prop.Value, out var propType))
            {
                continue;
            }

            script.Append("\"" + prop.Key + "\" " + propType);

            if (prop.Key != props.Last().Key)
            {
                script.Append(',');
            }
        }

        script.Append(')');

        return script.ToString();
    }

    private static Dictionary<Type, string> DataTypeMapper => new()
    {
        { typeof(bool), "INTEGER" },
        { typeof(byte), "INTEGER" },
        { typeof(byte[]), "BLOB" },
        { typeof(char), "TEXT" },
        { typeof(DateOnly), "TEXT" },
        { typeof(DateTime), "TEXT" },
        { typeof(DateTimeOffset), "TEXT" },
        { typeof(decimal), "TEXT" },
        { typeof(double), "REAL" },
        { typeof(Guid), "TEXT" },
        { typeof(short), "INTEGER" },
        { typeof(int), "INTEGER" },
        { typeof(long), "INTEGER" },
        { typeof(sbyte), "INTEGER" },
        { typeof(float), "REAL" },
        { typeof(string), "TEXT" },
        { typeof(TimeOnly), "TEXT" },
        { typeof(TimeSpan), "TEXT" },
        { typeof(ushort), "INTEGER" },
        { typeof(uint), "INTEGER" },
        { typeof(ulong), "INTEGER" }
    };
}
