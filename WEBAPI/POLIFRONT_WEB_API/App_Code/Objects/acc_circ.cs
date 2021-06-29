namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class acc_circ
    {
        //private SqlDataReader reader { get; set; }
        public int id_acc_circ { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        private string _date_heure_ac;
        public string date_heure_ac {
            get
            {
                return _date_heure_ac;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_heure_ac = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_heure_ac = null;
                }

            }
        }
        public string departement_ac { get; set; }
        public string point_ac { get; set; }
        public string localite_ac { get; set; }
        public int permis2cond_ac { get; set; }
        public string etats_ivr_ac { get; set; }
        public string imp_dom_ac { get; set; }
        public string direction_ac { get; set; }
        public string etats_vehic_ac { get; set; }
        public string rue_ac { get; set; }
        public string commune_ac { get; set; }
        public string ville_ac { get; set; }
        public string description_ac { get; set; }
        private string _date_enreg_ac;
        public string date_enreg_ac
        {
            get
            {
                return _date_enreg_ac;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_enreg_ac = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_ac = null;
                }

            }
        }

        public acc_circ() { }
        //public acc_circ(SqlDataReader reader)
        //{
        //    //this.reader = reader;
        //    parse(reader);
        //}
        public acc_circ(DataRow row)
        {
            parse(row);
        }

        //private List<acc_circ> parse(SqlDataReader reader)
        //{
        //    List<acc_circ> accs = new List<acc_circ>();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            //acc_circ acc = new acc_circ();
        //            //acc.id_acc_circ = int.Parse(reader["id_acc_circ"].ToString());
        //            //acc.id_utilisateurs = int.Parse(reader["id_utilisateurs"].ToString());
        //            //acc.numero_interp = reader["numero_interp"].ToString();
        //            //acc.numero_migrant = reader["numero_migrant"].ToString();
        //            //acc.date_heure_ac = reader["date_heure_ac"].ToString();
        //            //acc.departement_ac = reader["departement_ac"].ToString();
        //            //acc.point_ac = reader["point_ac"].ToString();
        //            //acc.localite_ac = reader["localite_ac"].ToString();
        //            //acc.permis2cond_ac = int.Parse(reader["permis2cond_ac"].ToString());
        //            //acc.etats_ivr_ac = reader["etats_ivr_ac"].ToString();
        //            //acc.imp_dom_ac = reader["imp_dom_ac"].ToString();
        //            //acc.direction_ac = reader["direction_ac"].ToString();
        //            //acc.etats_vehic_ac = reader["etats_vehic_ac"].ToString();
        //            //acc.rue_ac = reader["rue_ac"].ToString();
        //            //acc.commune_ac = reader["commune_ac"].ToString();
        //            //acc.ville_ac = reader["ville_ac"].ToString();
        //            //acc.description_ac = reader["description_ac"].ToString();
        //            //acc.date_enreg_ac = reader["date_enreg_ac"].ToString();
        //            //accs.Add(acc);

        //            acc_circ acc = new acc_circ();
        //            id_acc_circ = int.Parse(reader["id_acc_circ"].ToString());
        //            id_utilisateurs = int.Parse(reader["id_utilisateurs"].ToString());
        //            numero_interp = reader["numero_interp"].ToString();
        //            numero_migrant = reader["numero_migrant"].ToString();
        //            date_heure_ac = reader["date_heure_ac"].ToString();
        //            departement_ac = reader["departement_ac"].ToString();
        //            point_ac = reader["point_ac"].ToString();
        //            localite_ac = reader["localite_ac"].ToString();
        //            permis2cond_ac = int.Parse(reader["permis2cond_ac"].ToString());
        //            etats_ivr_ac = reader["etats_ivr_ac"].ToString();
        //            imp_dom_ac = reader["imp_dom_ac"].ToString();
        //            direction_ac = reader["direction_ac"].ToString();
        //            etats_vehic_ac = reader["etats_vehic_ac"].ToString();
        //            rue_ac = reader["rue_ac"].ToString();
        //            commune_ac = reader["commune_ac"].ToString();
        //            ville_ac = reader["ville_ac"].ToString();
        //            description_ac = reader["description_ac"].ToString();
        //            date_enreg_ac = reader["date_enreg_ac"].ToString();
        //            //accs.Add(acc);
        //            accs.Add(this);
        //        }
        //    }
        //    return accs;
        //}

        private acc_circ parse(DataRow row)
        {
            //acc_circ acc = new acc_circ();
            id_acc_circ = int.Parse(row["id_acc_circ"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            numero_interp = row["numero_interp"].ToString();
            numero_migrant = row["numero_migrant"].ToString();
            date_heure_ac = row["date_heure_ac"].ToString();
            departement_ac = row["departement_ac"].ToString();
            point_ac = row["point_ac"].ToString();
            localite_ac = row["localite_ac"].ToString();
            permis2cond_ac = int.Parse(row["permis2cond_ac"].ToString());
            etats_ivr_ac = row["etats_ivr_ac"].ToString();
            imp_dom_ac = row["imp_dom_ac"].ToString();
            direction_ac = row["direction_ac"].ToString();
            etats_vehic_ac = row["etats_vehic_ac"].ToString();
            rue_ac = row["rue_ac"].ToString();
            commune_ac = row["commune_ac"].ToString();
            ville_ac = row["ville_ac"].ToString();
            description_ac = row["description_ac"].ToString();
            date_enreg_ac = row["date_enreg_ac"].ToString();
            return this;
        }

        public static int addAccCirc(acc_circ acc)
        {
            try
            {
                string sql = @"INSERT INTO acc_circ
                            (id_utilisateurs
                            ,numero_interp
                            ,numero_migrant
                            ,date_heure_ac
                            ,departement_ac
                            ,point_ac
                            ,localite_ac
                            ,permis2cond_ac
                            ,etats_ivr_ac
                            ,imp_dom_ac
                            ,direction_ac
                            ,etats_vehic_ac
                            ,rue_ac
                            ,commune_ac
                            ,ville_ac
                            ,description_ac
                            ,date_enreg_ac)
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
                            ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    acc.id_utilisateurs,
                    acc.numero_interp,
                    acc.numero_migrant,
                    acc.date_heure_ac,
                    acc.departement_ac,
                    acc.point_ac,
                    acc.localite_ac,
                    acc.permis2cond_ac,
                    acc.etats_ivr_ac,
                    acc.imp_dom_ac,
                    acc.direction_ac,
                    acc.etats_vehic_ac,
                    acc.rue_ac,
                    acc.commune_ac,
                    acc.ville_ac,
                    acc.description_ac);
                return stmt.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }

        //internal static Dictionary<string, string> ajouteracc_circ(acc_circ acc)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
