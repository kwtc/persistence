namespace Kwtc.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class DataReaderExtensions
    {
        public static T ReadFirstOrDefault<T>(this IDataReader reader, Func<IDataRecord, T> recordReader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (recordReader == null)
            {
                throw new ArgumentNullException(nameof(recordReader));
            }

            return reader.Read() ? recordReader(reader) : default;
        }

        public static IEnumerable<T> ReadAll<T>(this IDataReader reader, Func<IDataRecord, T> recordReader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (recordReader == null)
            {
                throw new ArgumentNullException(nameof(recordReader));
            }

            var result = new List<T>();
            while (reader.Read())
            {
                result.Add(recordReader(reader));
            }

            return result;
        }
    }
}
