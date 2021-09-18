namespace Kwtc.Persistence
{
    using System;
    using System.Data;

    public static class DataRecordExtensions
    {
        public static T ConvertTo<T>(this IDataRecord record, string column)
        {
            var obj = record[column];
            if (obj == null || obj == DBNull.Value)
            {
                return default;
            }

            return (T) obj;
        }
    }
}
