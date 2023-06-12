namespace Kwtc.Persistence.Tests.Factories;

using System.Data;
using Kwtc.Persistence.Factories;
using Microsoft.Data.Sqlite;

public class InMemoryConnectionFactoryTests
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

    private static InMemoryConnectionFactory GetSut()
    {
        return new InMemoryConnectionFactory();
    }
}
