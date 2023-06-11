namespace Kwtc.Persistence.TypeHandlers;

using System.Data;
using Dapper;

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        // format parameter 0 return value as ISO 8601 string
        parameter.Value = value.ToString("O");
    }

    public override TimeOnly Parse(object value)
    {
        return TimeOnly.Parse((string)value);
    }
}
