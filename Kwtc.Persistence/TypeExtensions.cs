namespace Kwtc.Persistence;

using System.ComponentModel.DataAnnotations;
using System.Text;
using CommunityToolkit.Diagnostics;

public static class TypeExtensions
{
    public static string CreateTableScript(this Type type, string tableName)
    {
        Guard.IsNotNullOrEmpty(tableName, nameof(tableName));

        var fields = type.GetProperties().Select(p =>
            Attribute.IsDefined(p, typeof(KeyAttribute))
                ? new KeyValuePair<string, Type>(p.Name, typeof(KeyAttribute))
                : new KeyValuePair<string, Type>(p.Name, p.PropertyType)
        ).ToList();

        var script = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName}(");
        foreach (var field in fields)
        {
            var fieldType = DataTypeMapper.TryGetValue(field.Value, out var value) ? value : "BIGINT";

            script.Append("\"" + field.Key + "\" " + fieldType);

            if (field.Key != fields.Last().Key)
            {
                script.Append(',');
            }
        }

        script.Append(')');

        return script.ToString();
    }

    private static Dictionary<Type, string> DataTypeMapper => new()
    {
        { typeof(int), "INTEGER" },
        { typeof(long), "BIGINT" },
        { typeof(string), "NVARCHAR(8000)" },
        { typeof(bool), "BIT" },
        { typeof(DateTime), "NVARCHAR(8000)" },
        { typeof(float), "FLOAT" },
        { typeof(decimal), "DECIMAL(12,6)" },
        { typeof(Guid), "UNIQUEIDENTIFIER" }
    };
}
