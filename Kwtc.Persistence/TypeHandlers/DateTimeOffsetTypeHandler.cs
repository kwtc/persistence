namespace Kwtc.Persistence.TypeHandlers;

using System.Data;
using Dapper;

public class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
    {
        parameter.Value = value.ToString();
    }

    public override DateTimeOffset Parse(object value)
    {
        return DateTimeOffset.Parse((string)value);
    }
}
