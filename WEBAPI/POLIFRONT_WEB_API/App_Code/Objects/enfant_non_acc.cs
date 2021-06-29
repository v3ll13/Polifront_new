namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class enfant_non_acc
    {
        public int id_enfant_non_acc { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_ena { get; set; }
        public string departement_ena { get; set; }
        public string point_ena { get; set; }
        public string localite_ena { get; set; }
        public string provenance_ena { get; set; }
        public string typ_vulnerabilite_ena { get; set; }
        public string referencement_ena { get; set; }
        public System.DateTime date_enreg_ena { get; set; }

        public enfant_non_acc(DataRow row)
        {
            parse(row);
        }

        private enfant_non_acc parse(DataRow row)
        {
            id_enfant_non_acc = int.Parse(row["id_enfant_non_acc"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addEnfantNonAccompagne(enfant_non_acc ena)
        {
            try
            {
                string sql = @"INSERT INTO enfant_non_acc
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_ena
                                ,departement_ena
                                ,point_ena
                                ,localite_ena
                                ,provenance_ena
                                ,typ_vulnerabilite_ena
                                ,referencement_ena
                                ,date_enreg_ena)
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
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    ena.id_utilisateurs,
                    ena.numero_interp,
                    ena.numero_migrant,
                    ena.date_heure_ena,
                    ena.departement_ena,
                    ena.point_ena,
                    ena.localite_ena,
                    ena.provenance_ena,
                    ena.typ_vulnerabilite_ena,
                    ena.referencement_ena);
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
