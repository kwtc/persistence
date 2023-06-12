namespace Kwtc.Persistence.Tests.Factories;

using Kwtc.Persistence.Factories;
using Microsoft.Extensions.Configuration;

public class MySqlSharedConnectionFactoryTests
{
    private readonly Mock<IConfiguration> configuration = new(MockBehavior.Strict);

    [Fact]
    public void GetAsync_InvalidKey_ShouldThrow()
    {
        var sut = this.GetSut();

        var act = () => sut.GetAsync(string.Empty);

        act.Should().ThrowAsync<ArgumentException>();
    }


    private MySqlConnectionFactory GetSut()
    {
        return new MySqlConnectionFactory(this.configuration.Object);
    }
}
