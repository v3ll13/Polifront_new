namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class traf_sus_pers
    {
        public int id_traf_sus_pers { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_tsp { get; set; }
        public string departement_tsp { get; set; }
        public string point_tsp { get; set; }
        public string localite_tsp { get; set; }
        public string provenance_tsp { get; set; }
        public string destination_tsp { get; set; }
        public int nbr2victim_tsp { get; set; }
        public System.DateTime date_enreg_tsp { get; set; }

        public traf_sus_pers(DataRow row)
        {
            parse(row);
        }

        private traf_sus_pers parse(DataRow row)
        {
            id_traf_sus_pers = int.Parse(row["id_traf_sus_pers"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addTrafficSuspectePersonne(traf_sus_pers traf)
        {
            try
            {
                string sql = @"INSERT INTO traf_sus_pers
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_tsp
                                ,departement_tsp
                                ,point_tsp
                                ,localite_tsp
                                ,provenance_tsp
                                ,destination_tsp
                                ,nbr2victim_tsp
                                ,date_enreg_tsp)
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
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    traf.id_utilisateurs,
                    traf.numero_interp,
                    traf.numero_migrant,
                    traf.date_heure_tsp,
                    traf.departement_tsp,
                    traf.point_tsp,
                    traf.localite_tsp,
                    traf.provenance_tsp,
                    traf.destination_tsp,
                    traf.nbr2victim_tsp);
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
