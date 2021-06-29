namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class traffic_armes
    {
        public int id_traffic_armes { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_taf { get; set; }
        public string departement_taf { get; set; }
        public string point_taf { get; set; }
        public string localite_taf { get; set; }
        public string photo_obj_taf { get; set; }
        public string type_armes_taf { get; set; }
        public string marque_taf { get; set; }
        public string calibre_taf { get; set; }
        public byte[] is_charged_taf { get; set; }
        public int numero_series_taf { get; set; }
        public int quantite_taf { get; set; }
        public string description_taf { get; set; }
        public Nullable<decimal> valeur_monetaire_taf { get; set; }
        public string lieu_saisi_taf { get; set; }
        public System.DateTime date_enreg_taf { get; set; }

        public traffic_armes(DataRow row)
        {
            parse(row);
        }

        private traffic_armes parse(DataRow row)
        {
            id_traffic_armes = int.Parse(row["id_traffic_armes"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addTrafficArmes(traffic_armes traf)
        {
            try
            {
                string sql = @"INSERT INTO dbo.traffic_armes
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_taf
                                ,departement_taf
                                ,point_taf
                                ,localite_taf
                                ,photo_obj_taf
                                ,type_armes_taf
                                ,marque_taf
                                ,calibre_taf
                                ,is_charged_taf
                                ,numero_series_taf
                                ,quantite_taf
                                ,description_taf
                                ,valeur_monetaire_taf
                                ,lieu_saisi_taf
                                ,date_enreg_taf)
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
                    traf.date_heure_taf,
                    traf.departement_taf,
                    traf.point_taf,
                    traf.localite_taf,
                    traf.photo_obj_taf,
                    traf.type_armes_taf,
                    traf.marque_taf,
                    traf.calibre_taf,
                    traf.is_charged_taf,
                    traf.numero_series_taf,
                    traf.quantite_taf,
                    traf.description_taf,
                    traf.valeur_monetaire_taf,
                    traf.lieu_saisi_taf);
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
