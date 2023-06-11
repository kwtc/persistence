namespace Kwtc.Persistence.TypeHandlers;

using Dapper;

public static class TypeMapperHelper
{
    public static void RegisterDefaultHandlers()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
        SqlMapper.AddTypeHandler(new DateTimeOffsetTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeSpanTypeHandler());
    }
}
