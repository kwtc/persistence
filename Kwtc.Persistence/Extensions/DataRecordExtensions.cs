namespace Kwtc.Persistence.Extensions;

using System;
using System.Data;
using CommunityToolkit.Diagnostics;

public static class DataRecordExtensions
{
    public static T? ConvertTo<T>(this IDataRecord record, string column)
    {
        Guard.IsNotNullOrEmpty(column, nameof(column));

        var obj = record[column];
        return obj == DBNull.Value ? default : (T)obj;
    }
}
