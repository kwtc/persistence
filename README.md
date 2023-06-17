![.NET Build and Test](https://github.com/kwtc/persistence/actions/workflows/ci.yml/badge.svg)

# Persistence
A collection of utilities for working with persistence in .NET using Dapper.

## Features
- [Connection factories](#factories)
- [In-memory database (SQLite)](#in-memory)
- [Dapper type mappers](#mappers)

## <a name="factories"></a>Connection factories
Factory implementations for MySql, MsSql and SQLite connections implementing an `IConnectionFactory` interface which expose the following methods:

```c#
Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);
Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default);
```

They are designed to be used with dependency injection taking a `Microsoft.Extensions.Configuration.IConfiguration` object which provides access to connection string configuration. When getting connections you have the options to use a configuration key with section support  e.g. `ImportantStrings:BestConnectionEver` or not which will default to using `ConnectionStrings:DefaultConnection`.

The factories are implemented using the following data providers:

| Connection type | Data provider |
| ---------- | ---------- |
| MsSql | `Microsoft.Data.SqlClient` |
| MySql | `MySql.Data` |
| SQLite | `Microsoft.Data.Sqlite` |

## <a name="in-memory"></a>In-memory database (SQLite)
Intended to be used for testing the In-memory database is built with SQLite and the factory class differs slightly from the others. It implements another interface `IInMemoryConnectionFactory` which extends `IDisposable` and exposes a single method:

```c#
Task<SqliteConnection> GetAsync(CancellationToken cancellationToken = default);
```

Here an actual SqliteConnection implementation is returned which is extended with convenience methods. When the factory is instantiated it itself instantiates a InMemoryDatabase object that needs to be disposed after use. The database is created with the following connection string.

```c#
$"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared";
```

Extension methods for SqliteConnection are provided which facilitates easy creation of very basic test database tables.

```c#
void CreateTable<T>(this SqliteConnection connection, string tableName);
Task CreateTableAsync<T>(this SqliteConnection connection, string tableName, CancellationToken cancellationToken = default);

void DropTable(this SqliteConnection connection, string tableName);
Task DropTableAsync(this SqliteConnection connection, string tableName, CancellationToken cancellationToken = default);
```

Could be extended in the future with attribute support for table keys etc.


Example:
```c#
using var factory = new InMemoryConnectionFactory();
var connection = await factory.GetAsync();

await connection.CreateTableAsync<TestModel>("TestTable");

public class TestModel
{
    public int Id { get; set; }
    public string Title { get; set; }
}
```

In this example we instantiate an in-memory SQLite database and create a table with the columns defined in the `TestModel` class `Id` and `Title`.

The following is a list of supported .NET types and the SQLite type that they are mapped to when using the `CreateTable` methods

| .NET | SQLite |
| ---- | ------ |
| bool | INTEGER |
| byte | INTEGER |
| byte[] | BLOB |
| char | TEXT |
| DateOnly | TEXT |
| DateTime | TEXT |
| DateTimeOffset | TEXT |
| decimal | TEXT |
| double | REAL |
| Guid | TEXT |
| short | INTEGER |
| int | INTEGER |
| long | INTEGER |
| sbyte | INTEGER |
| float | REAL |
| string | TEXT |
| TimeOnly | TEXT |
| TimeSpan | TEXT |
| ushort | INTEGER |
| uint | INTEGER |
| ulong | INTEGER |

## <a name="mappers"></a>Dapper type mappers
I ran into an issue where certain type conversions namely Guid and Date/Time types were not support by default using the `Microsoft.Data.Sqlite` provider. The solution was to implement some default type mappers using Dappers `SqlMapper` for the problematic types:

```c#
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
SqlMapper.AddTypeHandler(new GuidTypeHandler());
SqlMapper.AddTypeHandler(new DateTimeOffsetTypeHandler());
SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
SqlMapper.AddTypeHandler(new TimeSpanTypeHandler());
```

These mappers are very basic and you may wish to customize if use for other than testing. A helper method is included that registers all the default implementations:

```c#
TypeMapperHelper.RegisterDefaultHandlers();
```


