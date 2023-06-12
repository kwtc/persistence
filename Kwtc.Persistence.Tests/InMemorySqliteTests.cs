namespace Kwtc.Persistence.Tests;

using Dapper;
using Factories;
using FluentAssertions;
using TypeHandlers;

public class InMemorySqliteTests
{
    public InMemorySqliteTests()
    {
        TypeMapperHelper.RegisterDefaultHandlers();
    }

    [Fact]
    public async Task Connection_CreateTableWithAllSupportedDataTypes_ShouldReturnModelWithValues()
    {
        const string tableName = "TestTable";
        using var factory = new InMemorySqliteConnectionFactory();
        var connection = await factory.GetAsync(CancellationToken.None);

        var model = new TestModel
        {
            Bool = true,
            Byte = 10,
            Bytes = new byte[] { 10, 2, 3 },
            Char = 'a',
            DateOnly = new DateOnly(2021, 1, 1),
            DateTime = DateTime.UtcNow,
            DateTimeOffset = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
            Decimal = 1.12m,
            Double = 1.12,
            Float = 1.12f,
            Guid = Guid.NewGuid(),
            Int = 10,
            Long = 10,
            Sbyte = 10,
            Short = 10,
            String = "test",
            TimeOnly = TimeOnly.FromDateTime(DateTime.UtcNow),
            TimeSpan = TimeSpan.FromHours(1),
            Uint = 10,
            Ulong = 10,
            Ushort = 10
        };

        await connection.CreateTableAsync<TestModel>(tableName);

        await connection.ExecuteAsync(
            @$"INSERT INTO {tableName} (Bool, Byte, Bytes, Char, DateOnly, DateTime, DateTimeOffset, Decimal, Double, Float, Guid, Short, Int, Long, Sbyte, String, TimeOnly, TimeSpan, Uint, Ulong, Ushort) 
                VALUES (@Bool, @Byte, @Bytes, @Char, @DateOnly, @DateTime, @DateTimeOffset, @Decimal, @Double, @Float, @Guid, @Short, @Int, @Long, @Sbyte, @String, @TimeOnly, @TimeSpan, @Uint, @Ulong, @Ushort)",
            new
            {
                model.Bool,
                model.Byte,
                model.Bytes,
                model.Char,
                model.DateOnly,
                model.DateTime,
                model.DateTimeOffset,
                model.Decimal,
                model.Double,
                model.Guid,
                model.Short,
                model.Int,
                model.Long,
                model.Sbyte,
                model.String,
                model.TimeOnly,
                model.TimeSpan,
                model.Uint,
                model.Ulong,
                model.Ushort,
                model.Float
            });

        var result = (await connection.QueryAsync<TestModel>($"SELECT * FROM {tableName}")).ToList();

        result.Should().ContainSingle();
        result.First().Bool.Should().Be(model.Bool);
        result.First().Byte.Should().Be(model.Byte);
        result.First().Bytes.Should().BeEquivalentTo(model.Bytes);
        result.First().Char.Should().Be(model.Char);
        result.First().DateOnly.Should().Be(model.DateOnly);
        result.First().DateTime.Should().Be(model.DateTime);
        result.First().DateTimeOffset.Should().Be(model.DateTimeOffset);
        result.First().Decimal.Should().Be(model.Decimal);
        result.First().Double.Should().Be(model.Double);
        result.First().Guid.Should().Be(model.Guid);
        result.First().Short.Should().Be(model.Short);
        result.First().Int.Should().Be(model.Int);
        result.First().Long.Should().Be(model.Long);
        result.First().Sbyte.Should().Be(model.Sbyte);
        result.First().Float.Should().Be(model.Float);
        result.First().String.Should().Be(model.String);
        result.First().TimeOnly.Should().Be(model.TimeOnly);
        result.First().TimeSpan.Should().Be(model.TimeSpan);
        result.First().Ushort.Should().Be(model.Ushort);
        result.First().Uint.Should().Be(model.Uint);
        result.First().Ulong.Should().Be(model.Ulong);
    }

    private class TestModel
    {
        public bool Bool { get; set; }
        public byte Byte { get; set; }
        public byte[] Bytes { get; set; } = default!;
        public char Char { get; set; }
        public DateOnly DateOnly { get; set; }
        public DateTime DateTime { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public Guid Guid { get; set; }
        public short Short { get; set; }
        public int Int { get; set; }
        public long Long { get; set; }
        public sbyte Sbyte { get; set; }
        public float Float { get; set; }
        public string String { get; set; } = default!;
        public TimeOnly TimeOnly { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public ushort Ushort { get; set; }
        public uint Uint { get; set; }
        public ulong Ulong { get; set; }
    }
}
