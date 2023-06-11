namespace Kwtc.Persistence.TypeHandlers;

using System.Data;
using Dapper;

public class TimeSpanTypeHandler : SqlMapper.TypeHandler<TimeSpan>
{
    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.Value = value.ToString();
    }

    public override TimeSpan Parse(object value)
    {
        return TimeSpan.Parse((string)value);
    }
}
