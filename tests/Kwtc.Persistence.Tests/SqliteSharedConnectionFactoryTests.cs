namespace Kwtc.Persistence.Tests;

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

public class SqliteSharedConnectionFactoryTests
{
    private readonly Mock<IConfiguration> configuration = new(MockBehavior.Strict);

    [Fact]
    public void GetAsync_InvalidKey_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.GetAsync(string.Empty);

        act.Should().ThrowAsync<ArgumentException>();
    }


    private SqliteSharedConnectionFactory GetSut()
    {
        return new SqliteSharedConnectionFactory(this.configuration.Object);
    }
}
