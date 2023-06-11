namespace Kwtc.Persistence.Tests;

using System.Data;
using FluentAssertions;
using Microsoft.Data.Sqlite;

public class InMemorySqliteConnectionFactoryTests
{
    [Fact]
    public async Task GetAsync_WhenCalled_ShouldReturnConnection()
    {
        // Arrange
        using var sut = GetSut();

        // Act
        var result = await sut.GetAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IDbConnection>();
        result.Should().BeOfType<SqliteConnection>();
    }
    
    private static InMemorySqliteConnectionFactory GetSut()
    {
        return new InMemorySqliteConnectionFactory();
    }
}
