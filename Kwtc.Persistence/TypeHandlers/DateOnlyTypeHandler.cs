namespace Kwtc.Persistence.TypeHandlers;

using System.Data;
using Dapper;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToString();
    }

    public override DateOnly Parse(object value)
    {
        return DateOnly.Parse((string)value);
    }
}
