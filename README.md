![example workflow](https://github.com/kwtc/persistence/actions/workflows/dotnet.yml/badge.svg)

# Persistence
A collection of utilities and extension methods for working with persistence in .NET using Dapper.

## Features
- [MsSql connection factory](#mssql)
- [MySql connection factory](#mysql)
- [Sqlite connection factory](#sqlite)
- [In memory connection factory (Sqlite)](#inmemory)
- [Sql type mappers](#mappers)

## MsSql, MySql and Sqlite factory classes
Implemented using Dapper and dependency injection of `Microsoft.Extensions.Configuration.IConfiguration` providing access to connection string configurations. Implements an `IConnectionFactory` interface that exposes two `GetAsync` methods. One creating a connection from a default connection string `ConnectionStrings:DefaultConnection` and the other offer the option to provide a custom configuration key with section support e.g. `ImportantStrings:BestConnectionEver`. 

## <a name="mssql"></a>MsSql connection factory
MsSql uses `Microsoft.Data.SqlClient` provider.

## <a name="mysql"></a>MySql connection factory
MySql uses `MySql.Data` provider.

## <a name="sqlite"></a>Sqlite connection factory
Sqlite uses `Microsoft.Data.Sqlite` provider.

## <a name="inmemory"></a>In memory connection factory (Sqlite)
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
