namespace Kwtc.Persistence.Tests;

using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

public class MsSqlSharedConnectionFactoryTests
{
    private readonly Mock<IConfiguration> configuration = new(MockBehavior.Strict);

    [Fact]
    public void GetAsync_InvalidKey_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.GetAsync(string.Empty);

        act.Should().ThrowAsync<ArgumentException>();
    }


    private MsSqlConnectionFactory GetSut()
    {
        return new MsSqlConnectionFactory(this.configuration.Object);
    }
}
