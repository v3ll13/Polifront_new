namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public partial class saisi_argent
    {
        public int id_saisi_argent { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public System.DateTime date_heure_sa { get; set; }
        public string departement_sa { get; set; }
        public string point_sa { get; set; }
        public string localite_sa { get; set; }
        public string devise_sa { get; set; }
        public string autres_devise_sa { get; set; }
        public int quantite_sa { get; set; }
        public string photo_sa { get; set; }
        public System.DateTime date_enreg_sa { get; set; }

        public saisi_argent(DataRow row)
        {
            parse(row);
        }

        private saisi_argent parse(DataRow row)
        {
            id_saisi_argent = int.Parse(row["id_saisi_argent"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            //...
            return this;
        }

        public static int addSaisiArgent(saisi_argent saisi)
        {
            try
            {
                string sql = @"INSERT INTO saisi_argent
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,date_heure_sa
                                ,departement_sa
                                ,point_sa
                                ,localite_sa
                                ,devise_sa
                                ,autres_devise_sa
                                ,quantite_sa
                                ,photo_sa
                                ,date_enreg_sa)
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
                    saisi.id_utilisateurs,
                    saisi.numero_interp,
                    saisi.numero_migrant,
                    saisi.date_heure_sa,
                    saisi.departement_sa,
                    saisi.point_sa,
                    saisi.localite_sa,
                    saisi.devise_sa,
                    saisi.autres_devise_sa,
                    saisi.quantite_sa,
                    saisi.photo_sa);
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
