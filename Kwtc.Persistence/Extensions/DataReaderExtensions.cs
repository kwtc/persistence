namespace Kwtc.Persistence.Extensions;

using System;
using System.Collections.Generic;
using System.Data;

public static class DataReaderExtensions
{
    public static T? ReadFirstOrDefault<T>(this IDataReader reader, Func<IDataRecord, T> recordReader)
    {
        return reader.Read() ? recordReader(reader) : default;
    }

    public static IEnumerable<T> ReadAll<T>(this IDataReader reader, Func<IDataRecord, T> recordReader)
    {
        while (reader.Read())
        {
            yield return recordReader(reader);
        }
    }
}
