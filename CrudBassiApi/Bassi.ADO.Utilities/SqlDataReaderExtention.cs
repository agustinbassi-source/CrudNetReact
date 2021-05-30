using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bassi.ADO.Utilities
{
    public static class SqlDataReaderExtention
    {
        public static bool GetBool(this SqlDataReader reader, string columnName)
        {
            return (bool)reader[columnName];
        }

        public static bool? GetNullBool(this SqlDataReader reader, string columnName)
        {
            return reader[columnName] as bool?;
        }

        public static short GetInt16(this SqlDataReader reader, string columnName)
        {
            return (short)reader[columnName];
        }

        public static short? GetNullInt16(this SqlDataReader reader, string columnName)
        {
            return reader[columnName] as short?;
        }

        public static int GetInt32(this SqlDataReader reader, string columnName)
        {
            return (int)reader[columnName];
        }

        public static int? GetNullInt32(this SqlDataReader reader, string columnName)
        {
            return reader[columnName] as int?;
        }

        public static decimal GetDecimal(this SqlDataReader reader, string columnName) 
        {
            return (decimal)reader[columnName];
        }

        public static decimal? GetNullDecimal(this SqlDataReader reader, string columnName) 
        {
            return reader[columnName] as decimal?;
        }

        public static long GetInt64(this SqlDataReader reader, string columnName)
        {
            return (long)reader[columnName];
        }

        public static long? GetNullInt64(this SqlDataReader reader, string columnName)
        {
            return reader[columnName] as long?;
        }

        public static string GetString(this SqlDataReader reader, string columnName)
        {
            return (string)reader[columnName];
        }

        public static string GetNullString(this SqlDataReader reader, string columnName)
        {
            return reader[columnName] as string;
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string columnName) 
        {
            return (DateTime)reader[columnName];
        }

        public static DateTime? GetNullDateTime(this SqlDataReader reader, string columnName) 
        {
            return reader[columnName] as DateTime?;
        }

        public static Guid GetGuid(this SqlDataReader reader, string columnName) 
        {
            return (Guid)reader[columnName];
        }

        public static Guid? GetNullGuid(this SqlDataReader reader, string columnName) 
        {
            return reader[columnName] as Guid?;
        }

    }
}
