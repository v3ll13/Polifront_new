namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class agressions
    {
        public int id_agressions { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        private string _date_heure_agr;
        public string date_heure_agr
        {
            get
            {
                return _date_heure_agr;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_heure_agr = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_heure_agr = null;
                }

            }
        }
        public string departement_agr { get; set; }
        public string point_agr { get; set; }
        public string localite_agr { get; set; }
        public string typ_agr { get; set; }
        public int nbr_victim_agr { get; set; }
        public string temoins_agr { get; set; }
        public string details_agr { get; set; }
        private string _date_enreg_agr;
        public string date_enreg_agr
        {
            get
            {
                return _date_enreg_agr;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_enreg_agr = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_heure_agr = null;
                }

            }
        }

        public agressions() { }
        public agressions(DataRow row)
        {
            parse(row);
        }

        private agressions parse(DataRow row)
        {
            id_agressions = int.Parse(row["id_agressions"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            numero_interp = row["numero_interp"].ToString();
            numero_migrant = row["numero_migrant"].ToString();
            date_heure_agr = row["date_heure_agr"].ToString();
            departement_agr = row["departement_agr"].ToString();
            point_agr = row["point_agr"].ToString();
            localite_agr = row["localite_agr"].ToString();
            typ_agr = row["typ_agr"].ToString();
            nbr_victim_agr = int.Parse(row["nbr_victim_agr"].ToString());
            temoins_agr = row["temoins_agr"].ToString();
            details_agr = row["details_agr"].ToString();
            date_enreg_agr = row["date_enreg_agr"].ToString();
            return this;
        }

        public static int addAgression(agressions agr)
        {
            try
            {
                string sql = @"INSERT INTO dbo.agressions
                            (id_utilisateurs
                            ,numero_interp
                            ,numero_migrant
                            ,date_heure_agr
                            ,departement_agr
                            ,point_agr
                            ,localite_agr
                            ,typ_agr
                            ,nbr_victim_agr
                            ,temoins_agr
                            ,details_agr
                            ,date_enreg_agr)
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
                            ,DETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    agr.id_utilisateurs,
                    agr.numero_interp,
                    agr.numero_migrant,
                    agr.date_heure_agr,
                    agr.departement_agr,
                    agr.point_agr,
                    agr.localite_agr,
                    agr.typ_agr,
                    agr.nbr_victim_agr,
                    agr.temoins_agr,
                    agr.details_agr
                    );
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
