namespace Kwtc.Persistence.Tests;

using System.Data;
using FluentAssertions;
using Moq;

public class DataRecordExtensionsTests
{
    [Fact]
    public void ConvertTo_InvalidColumn_ShouldThrow()
    {
        var sut = GetSut().Object;

        var act = () => sut.ConvertTo<string>(string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ConvertTo_ValueIsDBNull_ShouldReturnDefault()
    {
        const string column = "Column";
        var sut = GetSut();
        sut.Setup(reader => reader[column]).Returns(DBNull.Value);

        var result = sut.Object.ConvertTo<object>(column);

        result.Should().Be(default);
    }

    [Fact]
    public void ConvertTo_ValueIsNumber_ShouldReturnNumberAsString()
    {
        const string column = "Column";
        var sut = GetSut();
        sut.Setup(reader => reader[column]).Returns(1337);

        var result = sut.Object.ConvertTo<object>(column);

        result.Should().Be(1337);
    }

    private static Mock<IDataRecord> GetSut()
    {
        // Testing a static class is hard, so we'll just mock the IDataRecord
        return new Mock<IDataRecord>(MockBehavior.Strict);
    }
}
