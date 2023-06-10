namespace Kwtc.Persistence;

using System.Data;
using CommunityToolkit.Diagnostics;
using Dapper;

public static class DbConnectionExtensions
{
    public static void CreateTableIfNotExists<T>(this IDbConnection connection, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        connection.Execute(typeof(T).CreateTableScript(tableName));
    }

    public static void DropTableIfExists(this IDbConnection connection, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        connection.Execute($"DROP TABLE IF EXISTS {tableName}");
    }
}
