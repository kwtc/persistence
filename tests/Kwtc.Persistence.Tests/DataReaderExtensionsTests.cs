namespace Kwtc.Persistence.Tests;

using System.Data;
using FluentAssertions;
using Moq;

public class DataReaderExtensionsTests
{
    [Fact]
    public void ReadFirstOrDefault_UnableToRead_ShouldReturnDefault()
    {
        var sut = GetSut();
        sut.Setup(reader => reader.Read()).Returns(false);

        var result = sut.Object.ReadFirstOrDefault(record => record.GetString(1));

        result.Should().Be(default);
    }

    [Fact]
    public void ReadFirstOrDefault_AbleToRead_ShouldExecuteReader()
    {
        const string readerResult = "Value";
        var sut = GetSut();
        sut.Setup(reader => reader.Read()).Returns(true);

        var reader = new Mock<Func<IDataRecord, string>>();
        reader.Setup(func => func(It.IsAny<IDataRecord>())).Returns(readerResult);

        var result = sut.Object.ReadFirstOrDefault(reader.Object);

        result.Should().Be(readerResult);
    }

    [Fact]
    public void ReadAll_UnableToRead_ShouldReturnEmptyCollection()
    {
        var sut = GetSut();
        sut.Setup(reader => reader.Read()).Returns(false);

        var result = sut.Object.ReadAll(record => record.GetString(1));

        result.Should().BeEmpty();
    }

    [Fact]
    public void ReadAll_AbleToRead_ShouldReturnCollectionWithValue()
    {
        const string readerResult = "Value";
        var sut = GetSut();
        sut.SetupSequence(reader => reader.Read())
           .Returns(true)
           .Returns(false);

        var reader = new Mock<Func<IDataRecord, string>>();
        reader.Setup(func => func(It.IsAny<IDataRecord>())).Returns(readerResult);

        var result = sut.Object.ReadAll(reader.Object).ToList();

        result.Should().HaveCount(1);
        result.Should().Contain(readerResult);
    }

    private static Mock<IDataReader> GetSut()
    {
        // Testing a static class is hard, so we'll just mock the IDataReader
        return new Mock<IDataReader>(MockBehavior.Strict);
    }
}
