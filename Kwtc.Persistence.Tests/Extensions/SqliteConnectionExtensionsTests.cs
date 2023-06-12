namespace Kwtc.Persistence.Tests.Extensions;

using Microsoft.Data.Sqlite;
using Persistence.Extensions;

public class SqliteConnectionExtensionsTests
{
    [Fact]
    public void CreateTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.CreateTable<object>(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateTable_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        // sut.Setup(connection => connection.GetType()).Returns(typeof(SqliteConnection));
        // sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        // sut.Setup(connection => connection.Open());
        // sut.Setup(connection => connection.Close());
        // sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.CreateTable<DataModel>("TableName");

        // sut.Verify(connection => connection.Open(), Times.Exactly(2));
    }

    [Fact]
    public void DropTable_InvalidTableName_ShouldThrow()
    {
        var sut = GetSut();

        var act = () => sut.DropTable(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DropTable_ConnectionNotOpen_ShouldOpenConnection()
    {
        var sut = GetSut();
        // sut.Setup(connection => connection.State).Returns(ConnectionState.Closed);
        // sut.Setup(connection => connection.Open());
        // sut.Setup(connection => connection.Close());
        // sut.Setup(connection => connection.CreateCommand()).Returns(new Mock<IDbCommand>().Object);

        sut.CreateTable<DataModel>("TableName");

        // sut.Verify(connection => connection.Open(), Times.Exactly(2));
    }

    private static SqliteConnection GetSut()
    {
        // Testing a static class is hard, so we'll just mock the IDbConnection
        return new SqliteConnection();
    }

    private class DataModel
    {
        public int Id { get; set; }
    }
}
