namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class traffic_munitions
    {
        public int id_traffic_munitions { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_tm { get; set; }
        public string departement_tm { get; set; }
        public string point_tm { get; set; }
        public string localite_tm { get; set; }
        public string photo_obj_tm { get; set; }
        public string type_munit_tm { get; set; }
        public string autres_tm { get; set; }
        public string referencement_tm { get; set; }
        public System.DateTime date_enreg_tm { get; set; }

        public traffic_munitions(DataRow row)
        {
            parse(row);
        }

        private traffic_munitions parse(DataRow row)
        {
            id_traffic_munitions = int.Parse(row["id_traffic_munitions"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addTrafficMunitions(traffic_munitions traf)
        {
            try
            {
                string sql = @"INSERT INTO traffic_munitions
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_tm
                                ,departement_tm
                                ,point_tm
                                ,localite_tm
                                ,photo_obj_tm
                                ,type_munit_tm
                                ,autres_tm
                                ,referencement_tm
                                ,date_enreg_tm)
                            VALUES
                                (?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    traf.id_utilisateurs,
                    traf.numero_interp,
                    traf.numero_migrant,
                    traf.date_heure_tm,
                    traf.departement_tm,
                    traf.point_tm,
                    traf.localite_tm,
                    traf.photo_obj_tm,
                    traf.type_munit_tm,
                    traf.autres_tm,
                    traf.referencement_tm);
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
