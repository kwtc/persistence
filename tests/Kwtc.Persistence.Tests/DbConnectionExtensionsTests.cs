namespace Kwtc.Persistence.Tests;

using System.Data;
using FluentAssertions;
using Moq;

public class DbConnectionExtensionsTests
{
    [Fact]
    public void CreateTableIfNotExists_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut().Object;

        var act = () => sut.CreateTableIfNotExists<object>(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateTableIfNotExists_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        sut.Setup(connection => connection.Open());
        sut.Setup(connection => connection.Close());
        sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.Object.CreateTableIfNotExists<DataModel>("TableName");

        sut.Verify(connection => connection.Open(), Times.Exactly(2));
    }

    [Fact]
    public void DropTableIfExists_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut().Object;

        var act = () => sut.DropTableIfExists(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DropTableIfExists_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        sut.Setup(connection => connection.Open());
        sut.Setup(connection => connection.Close());
        sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.Object.CreateTableIfNotExists<DataModel>("TableName");

        sut.Verify(connection => connection.Open(), Times.Exactly(2));
    }

    private static Mock<IDbConnection> GetSut()
    {
        // Testing a static class is hard, so we'll just mock the IDbConnection
        return new Mock<IDbConnection>(MockBehavior.Strict);
    }

    private class DataModel
    {
        public int Id { get; set; }
    }
}
