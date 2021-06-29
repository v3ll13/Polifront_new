namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class vol_de_voiture
    {
        public int id_vol_de_voiture { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_v2v { get; set; }
        public string departement_v2v { get; set; }
        public string point_v2v { get; set; }
        public string localite_v2v { get; set; }
        public string no_permis_v2v { get; set; }
        public string nom_suspect_v2v { get; set; }
        public byte[] is_find { get; set; }
        public System.DateTime date_enreg_v2v { get; set; }

        public vol_de_voiture(DataRow row)
        {
            parse(row);
        }
        private vol_de_voiture parse(DataRow row)
        {
            id_vol_de_voiture = int.Parse(row["id_vol_de_voiture"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addVolVoiture(vol_de_voiture vol)
        {
            try
            {
                string sql = @"INSERT INTO dbo.vol_de_voiture
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_v2v
                                ,departement_v2v
                                ,point_v2v
                                ,localite_v2v
                                ,no_permis_v2v
                                ,nom_suspect_v2v
                                ,is_find
                                ,date_enreg_v2v)
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
                    vol.id_utilisateurs,
                    vol.numero_interp,
                    vol.numero_migrant,
                    vol.date_heure_v2v,
                    vol.departement_v2v,
                    vol.point_v2v,
                    vol.localite_v2v,
                    vol.no_permis_v2v,
                    vol.nom_suspect_v2v,
                    vol.is_find);
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
