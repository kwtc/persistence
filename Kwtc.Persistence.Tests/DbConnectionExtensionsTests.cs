namespace Kwtc.Persistence.Tests;

using System.Data;
using FluentAssertions;
using Moq;

public class DbConnectionExtensionsTests
{
    [Fact]
    public void CreateTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut().Object;

        var act = () => sut.CreateTable<object>(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateTable_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        sut.Setup(connection => connection.Open());
        sut.Setup(connection => connection.Close());
        sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.Object.CreateTable<DataModel>("TableName");

        sut.Verify(connection => connection.Open(), Times.Exactly(2));
    }

    [Fact]
    public void DropTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut().Object;

        var act = () => sut.DropTable(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DropTable_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        sut.Setup(connection => connection.Open());
        sut.Setup(connection => connection.Close());
        sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.Object.CreateTable<DataModel>("TableName");

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
