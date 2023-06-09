namespace Kwtc.Persistence;

using System.ComponentModel.DataAnnotations;
using System.Text;

public static class TypeExtensions
{
    public static string CreateTableScript(this Type type, string tableName)
    {
        var fields = type.GetProperties().Select(p =>
            Attribute.IsDefined(p, typeof(KeyAttribute))
                ? new KeyValuePair<string, Type>(p.Name, typeof(KeyAttribute))
                : new KeyValuePair<string, Type>(p.Name, p.PropertyType)
        );

        var script = new StringBuilder();
        script.AppendLine("CREATE TABLE IF NOT EXISTS " + tableName);
        script.AppendLine("(");
        foreach (var field in fields)
        {
            var fieldType = DataTypeMapper.ContainsKey(field.Value) ? DataTypeMapper[field.Value] : "BIGINT";

            script.Append("\"" + field.Key + "\" " + fieldType);


            if (field.Key != fields.Last().Key)
            {
                script.Append(',');
            }

            script.Append(Environment.NewLine);
        }

        script.AppendLine(")");

        return script.ToString();
    }

    private static Dictionary<Type, string> DataTypeMapper => new()
    {
        { typeof(KeyAttribute), "INTEGER PRIMARY KEY AUTOINCREMENT" },
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
