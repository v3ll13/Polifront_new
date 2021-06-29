namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class passeur_suspecte
    {
        public int id_pass_susp { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_ps { get; set; }
        public string departement_ps { get; set; }
        public string point_ps { get; set; }
        public string localite_ps { get; set; }
        public string provenance_ps { get; set; }
        public string destination_ps { get; set; }
        public int nbr2victim_ps { get; set; }
        public System.DateTime date_enreg_ps { get; set; }

        public passeur_suspecte(DataRow row)
        {
            parse(row);
        }

        private passeur_suspecte parse(DataRow row)
        {
            id_pass_susp = int.Parse(row["id_pass_susp"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addPasseurSuspecte(passeur_suspecte pass)
        {
            try
            {
                string sql = @"INSERT INTO passeur_suspecte
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_ps
                                ,departement_ps
                                ,point_ps
                                ,localite_ps
                                ,provenance_ps
                                ,destination_ps
                                ,nbr2victim_ps
                                ,date_enreg_ps)
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
                    pass.id_utilisateurs,
                    pass.numero_interp,
                    pass.numero_migrant,
                    pass.date_heure_ps,
                    pass.departement_ps,
                    pass.point_ps,
                    pass.localite_ps,
                    pass.provenance_ps,
                    pass.destination_ps,
                    pass.nbr2victim_ps);
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
