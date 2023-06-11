namespace Kwtc.Persistence.TypeHandlers;

using System.Data;
using Dapper;

/// <summary>
/// Added to support Guids in SQLite
/// </summary>
public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value;
    }

    public override Guid Parse(object value)
    {
        return new Guid((string)value);
    }
}
