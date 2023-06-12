namespace Kwtc.Persistence.Tests;

using Factories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

public class MySqlSharedConnectionFactoryTests
{
    private readonly Mock<IConfiguration> configuration = new(MockBehavior.Strict);

    [Fact]
    public void GetAsync_InvalidKey_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.GetAsync(string.Empty);

        act.Should().ThrowAsync<ArgumentException>();
    }


    private MySqlConnectionFactory GetSut()
    {
        return new MySqlConnectionFactory(this.configuration.Object);
    }
}
