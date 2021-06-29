using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Db_Core
{
    public abstract class SqlConvert
    {
        public static DataTable ToDataTable(SqlDataReader reader)
        {
            DataTable result = new DataTable();
            result.Load(reader);
            return result;
        }

        public static DataTable ToDataTable(SqlDataReader reader, Type dataTableType)
        {
            DataTable result = (DataTable)Activator.CreateInstance(dataTableType);
            result.Load(reader);
            return result;
        }

        public static int? ToInt32(object value)
        {
            if (value == Convert.DBNull)
                return null;
            if (value == null)
                return null;
            return Convert.ToInt32(value);
        }

        public static long? ToInt64(object value)
        {
            if (value == Convert.DBNull)
                return null;
            if (value == null)
                return null;
            return Convert.ToInt64(value);
        }

        public static DateTime? ToDateTime(object value)
        {
            if (value == Convert.DBNull)
                return null;
            if (value == null)
                return null;
            return DateTime.Parse(value.ToString());
        }

        public static string ToString(object value)
        {
            return value as string;
        }

        public static decimal? ToDecimal(object value)
        {
            if (value == Convert.DBNull)
                return null;
            if (value == null)
                return null;
            return Convert.ToDecimal(value);
        }
    }
}
