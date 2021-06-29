namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class contrebande
    {
        public int id_contrebande { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        private string _date_heure_contb;
        public string date_heure_contb
        {
            get
            {
                return _date_heure_contb;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_heure_contb = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_heure_contb = null;
                }

            }
        }
        public string departement_contb { get; set; }
        public string point_contb { get; set; }
        public string localite_contb { get; set; }
        public string photo_march_contb { get; set; }
        public string typ_marchand_contb { get; set; }
        public string quantite_contb { get; set; }
        public string poids_contb { get; set; }
        public string val_monet_contb { get; set; }
        public string description_contb { get; set; }
        public string moyen_transp_contb { get; set; }
        private string _date_enreg_contb;
        public string date_enreg_contb
        {
            get
            {
                return _date_enreg_contb;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_enreg_contb = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_contb = null;
                }

            }
        }

        public contrebande(DataRow row)
        {
            parse(row);
        }

        private contrebande parse(DataRow row)
        {
            id_contrebande = int.Parse(row["id_contrebande"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            numero_interp = row["numero_interp"].ToString();
            numero_migrant = row["numero_migrant"].ToString();
            //...
            return this;
        }

        public static int addContrebande(contrebande con)
        {
            try
            {
                string sql = @"INSERT INTO dbo.contrebande
                            (id_utilisateurs
                            ,numero_interp
                            ,numero_migrant
                            ,date_heure_contb
                            ,departement_contb
                            ,point_contb
                            ,localite_contb
                            ,photo_march_contb
                            ,typ_marchand_contb
                            ,quantite_contb
                            ,poids_contb
                            ,val_monet_contb
                            ,description_contb
                            ,moyen_transp_contb
                            ,date_enreg_contb)
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
                            ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    con.id_utilisateurs,
                    con.numero_interp,
                    con.numero_migrant,
                    con.date_heure_contb,
                    con.departement_contb,
                    con.point_contb,
                    con.localite_contb,
                    con.photo_march_contb,
                    con.typ_marchand_contb,
                    con.quantite_contb,
                    con.poids_contb,
                    con.val_monet_contb,
                    con.description_contb,
                    con.moyen_transp_contb);
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
