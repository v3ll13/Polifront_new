using Db_Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace POLIFRONT_WEB_API.App_Code
{
    public class StatObj
    {
        public string title { get; set; }
        public int quantity { get; set; }

        public static List<StatObj> getStat()
        {
            List<StatObj> stats = new List<StatObj>();

            List<string> tables = Utils.tableList;

            for(int i = 0; i < tables.Count; i++)
            {
                StatObj stat = new StatObj();
                try
                {
                    string sql = @"SELECT COUNT(*) as 'count' FROM " + tables[i];
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);

                    SqlDataReader reader = stmt.ExecuteReader();
                    if (reader.Read())
                    {
                        stat.quantity = int.Parse(reader["count"].ToString());
                        stat.title = tables[i];
                        stats.Add(stat);
                    }
                    //if(stat.quantity > 0)
                    //{
                    //    stat.title = tables[i];
                    //    stats.Add(stat);
                    //}
                }
                catch(Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name + " | " + tables[i], e);
                    continue;
                }
            }

            return stats;
        }
    }
}