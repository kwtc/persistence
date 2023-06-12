namespace Kwtc.Persistence.Tests.Extensions;

using Microsoft.Data.Sqlite;
using Persistence.Extensions;

public class SqliteConnectionExtensionsTests
{
    [Fact]
    public void CreateTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.CreateTable<TestModel>(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateTable_ValidTableName_ShouldNotThrow()
    {
        var sut = GetSut();

        var act = () => sut.CreateTable<TestModel>("ValidTableName");

        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public async Task CreateTableAsync_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.CreateTableAsync<TestModel>(string.Empty);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task CreateTableAsync_ValidTableName_ShouldNotThrow()
    {
        var sut = GetSut();

        var act = () => sut.CreateTableAsync<TestModel>("ValidTableName");

        await act.Should().NotThrowAsync<ArgumentException>();
    }

    [Fact]
    public void DropTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.DropTable(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DropTable_ValidTableName_ShouldNotThrow()
    {
        var sut = GetSut();

        var act = () => sut.DropTable("ValidTableName");

        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public async Task DropTableAsync_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.DropTableAsync(string.Empty);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task DropTableAsync_ValidTableName_ShouldNotThrow()
    {
        var sut = GetSut();

        var act = () => sut.DropTableAsync("ValidTableName");

        await act.Should().NotThrowAsync<ArgumentException>();
    }

    private static SqliteConnection GetSut()
    {
        return new SqliteConnection();
    }

    private class TestModel
    {
        public int Id { get; set; }
    }
}
