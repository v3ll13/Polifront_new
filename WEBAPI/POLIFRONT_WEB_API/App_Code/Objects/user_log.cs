namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class user_log
    {
        public int id_user_log { get; set; }
        public int id_utilisateurs { get; set; }
        public string nom_utils { get; set; }
        public DateTime loginU { get; set; }
        public DateTime logoutU { get; set; }
        public string reason { get; set; }



        public static int insertUserLogin(user_log log)
        {
            try
            {
                string sql = @"insert into user_log(id_utilisateurs, nom_utils, loginU) values(?, ?, getdate())";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);

                stmt.SetParameters(log.id_utilisateurs, log.nom_utils);

                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }
    }
}
