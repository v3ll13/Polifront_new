using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Db_Core
{
    public enum SqlConnString
    {
        POLIFRONT
    };

    public abstract class SqlCommon
    {
        public static string[] ConnectionStrings = new string[]
        {
            //"Data Source=localhost;Initial Catalog=polifrontdb;User ID=polifront_admin;Password=at-Q3+)KJ!~xWfp_;Integrated Security=True;"
             "Data Source= DESKTOP-R7CB6TG\\OIMINSTANCE; Initial Catalog=polifrontdb;  Integrated Security=True;  "


    };

        public static string GetConnectionString(SqlConnString str)
        {
            return ConnectionStrings[(int)str];
        }
    }
}


