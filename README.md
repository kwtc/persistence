![.NET Build and Test](https://github.com/kwtc/persistence/actions/workflows/ci.yml/badge.svg)

# Persistence
A collection of utilities for working with persistence in .NET using Dapper.

## Features
- [Connection factories](#factories)
- [In-memory database (SQLite)](#in-memory)
- [Dapper type mappers](#mappers)

## <a name="factories"></a>Connection factories
Factory implementations for MySql, MsSql and SQLite connections implementing an `IConnectionFactory` interface which exposes the following methods:

```c#
Task<IDbConnection> GetAsync(CancellationToken cancellationToken = default);
Task<IDbConnection> GetAsync(string configKey, CancellationToken cancellationToken = default);
```

They are designed to be used with dependency injection taking a `Microsoft.Extensions.Configuration.IConfiguration` object which provides access to connection string configuration. When getting connections you have the option to use pass a configuration key with section support  e.g. `ImportantStrings:BestConnectionEver` or use the default `ConnectionStrings:DefaultConnection`.

The factories are implemented using the following data providers:

| Connection type | Data provider |
| ---------- | ---------- |
| MsSql | `Microsoft.Data.SqlClient` |
| MySql | `MySql.Data` |
| SQLite | `Microsoft.Data.Sqlite` |

## <a name="in-memory"></a>In-memory database (SQLite)
The in-memory connection factory is implemented with Sqlite and differs from the other factories in that it implements another interface `IInMemoryConnectionFactory` which extends `IDisposable`. Here I opted for hardcoding a connection string in the factory constructor:

```c#
this.connectionString = $"Data Source={Guid.NewGuid():N};Mode=Memory;Cache=Shared";
```

My reasoning being that this would be convenient since I expect it to only be used for testing purposes. If I'm wrong then a custom implementation can easily be created using the `SqliteConnectionFactory`.

Since the connection is created when constructing the factory <b>remember to dispose</b>.

## <a name="mappers"></a>Sql type mappers
I ran into an issue where certain type conversions namely Guid and Date/Time types were not support by default using the `Microsoft.Data.Sqlite` provider. My solution was to implement some default type mappers using Dappers `SqlMapper` for the problematic types:

```c#
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
SqlMapper.AddTypeHandler(new GuidTypeHandler());
SqlMapper.AddTypeHandler(new DateTimeOffsetTypeHandler());
SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
SqlMapper.AddTypeHandler(new TimeSpanTypeHandler());
```

These mappers are very basic and you may wish to customize. A helper method is included that registers all the default implementations:

```c#
TypeMapperHelper.RegisterDefaultHandlers();
```
