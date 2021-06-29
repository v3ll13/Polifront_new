namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class suivi
    {
        public int id_suivi { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_migrant { get; set; }
        public string numero_interp { get; set; }
        public string description_suivi { get; set; }
        public DateTime date_suivi { get; set; }
        public DateTime date_enreg_suivi { get; set; }

        public suivi(DataRow row)
        {
            parse(row);
        }

        private suivi parse(DataRow row)
        {
            id_suivi = int.Parse(row["id_suivi"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addSuivi(suivi sui)
        {
            try
            {
                string sql = @"INSERT INTO suivi
                                (id_utilisateurs
                                ,numero_migrant
                                ,numero_interp
                                ,description_suivi
                                ,date_suivi
                                ,date_enreg_suivi)
                            VALUES
                                (?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    sui.id_utilisateurs,
                    sui.numero_migrant,
                    sui.numero_interp,
                    sui.description_suivi,
                    sui.date_suivi);
                return stmt.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }
    }
}
