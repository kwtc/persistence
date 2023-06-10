namespace Kwtc.Persistence.Tests;

using Dapper;
using FluentAssertions;

public class InMemorySqliteTests
{
    public InMemorySqliteTests()
    {
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    [Fact]
    public async Task Connection_CreateTableAndPopulate_ShouldReturnModelWithValues()
    {
        const string tableName = "TestTable";
        var factory = new InMemorySqliteConnectionFactory();
        var connection = await factory.GetAsync(CancellationToken.None);

        var model = new TestModel
        {
            Int = 1,
            Long = 2,
            String = "3",
            Bool = true,
            DateTime = DateTime.Now,
            Float = 4.0f,
            Decimal = 5.0m,
            Guid = new Guid("39629331-3192-4872-BF94-B1B79EA8D2D8")
        };

        var script = typeof(TestModel).CreateTableScript(tableName);
        await connection.ExecuteAsync(script);

        await connection.ExecuteAsync($"INSERT INTO {tableName} VALUES (@Int, @Long, @String, @Bool, @DateTime, @Float, @Decimal, @Guid)", model);

        var result = (await connection.QueryAsync<TestModel>($"SELECT * FROM {tableName}")).ToList();

        result.Should().ContainSingle();
        result.First().Int.Should().Be(model.Int);
        result.First().Long.Should().Be(model.Long);
        result.First().String.Should().Be(model.String);
        result.First().Bool.Should().Be(model.Bool);
        result.First().DateTime.Should().Be(model.DateTime);
        result.First().Float.Should().Be(model.Float);
        result.First().Decimal.Should().Be(model.Decimal);
        result.First().Guid.Should().Be(model.Guid);
    }

    private class TestModel
    {
        public int Int { get; set; }
        public long Long { get; set; }
        public string String { get; set; } = default!;
        public bool Bool { get; set; }
        public DateTime DateTime { get; set; }
        public float Float { get; set; }
        public decimal Decimal { get; set; }
        public Guid Guid { get; set; }
    }
}
