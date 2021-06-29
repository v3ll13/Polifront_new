namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class migrant
    {
        /*
          [id_migrant]
          [numero_migrant]
          [photo_migrant]
          [nom_migrant]
          [prenom_migrant]
          [surnom_migrant]
          [ethnicite_migrant]
          [sexe_migrant]
          [dat2naiss_migrant]
          [age_migrant]
          [pas2piece]
          [cin_migrant]
          [passeport_migrant]
          [permis2cond_migrant]
          [nif_migrant]
          [nom_aut_piec_mig]
          [description_vestim]
          [remarques]
          [date_enreg_migrant]
         */
        public int id_migrant { get; set; }
        //public int id_utilisateurs { get; set; }
        public string code_migrant { get; set; }
        public string photo_migrant { get; set; }
        public string nom_migrant { get; set; }
        public string prenom_migrant { get; set; }
        public string surnom_migrant { get; set; }
        public string ethnicite_migrant { get; set; }
        public string sexe_migrant { get; set; }
        private string _dat2naiss_migrant;
        public string dat2naiss_migrant
        {
            get
            {
                return _dat2naiss_migrant.ToString();
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _dat2naiss_migrant = date_naissance.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    _dat2naiss_migrant = null;
                }

            }
        }
        //public int age_migrant { get; set; }
        private List<string> _nationalite_migrant;
        public List<string> nationalite_migrant
        {
            get
            {
                return _nationalite_migrant;
            }
            set
            {
                if (id_migrant == 0)
                {
                    _nationalite_migrant = value;
                }
                else
                {
                    _nationalite_migrant = getListNationalite(id_migrant, value);
                }

            }
        }
        public int pas2piece { get; set; }
        public string cin_migrant { get; set; }
        public int passeport_migrant { get; set; }
        public string permis2cond_migrant { get; set; }
        public string nif_migrant { get; set; }
        //public List<string> nom_aut_piec_mig { get; set; }
        public List<string> nom_aut_piec_mig = new List<string>();
        private List<string> _adress_migrant;
        public List<string> adress_migrant
        {
            get
            {
                return _adress_migrant;
            }
            set
            {
                if (id_migrant == 0)
                {
                    _adress_migrant = value;
                }
                else
                {
                    _adress_migrant = getListAdresse(id_migrant, value);
                }

            }
        }
        private List<string> _num_tel_migrant;
        public List<string> num_tel_migrant
        {
            get
            {
                return _num_tel_migrant;
            }
            set
            {
                if (id_migrant == 0)
                {
                    _num_tel_migrant = value;
                }
                else
                {
                    _num_tel_migrant = getListTelephone(id_migrant, value);
                }

            }
        }
        public string description_vestim { get; set; }
        public string remarques { get; set; }
        private string _date_enreg_migrant;
        public string date_enreg_migrant
        {
            get
            {
                return _date_enreg_migrant;
            }
            set
            {
                if (value != null)
                {
                    var date_insert = DateTime.Parse(value);
                    _date_enreg_migrant = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_migrant = null;
                }

            }
        }

        #region List infos migrant
        private List<string> getListTelephone(int id_migrant, List<string> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_telephone WHERE id_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id_migrant);
                return parseTelephone(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<string> getListNationalite(int id_migrant, List<string> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_nationalite WHERE id_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id_migrant);
                return parseNationalite(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<string> getListAdresse(int id_migrant, List<string> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_adresse WHERE id_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id_migrant);
                return parseAdresse(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<string> parseTelephone(SqlDataReader reader, List<string> value)
        {
            List<string> phones = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    phones.Add(reader["telephone"].ToString());
                }
            }
            else
            {
                if (value == null)
                {
                    phones = new List<string>();
                }
                else
                {
                    phones = value;
                }
            }

            return phones;
        }
        private List<string> parseNationalite(SqlDataReader reader, List<string> value)
        {
            List<string> nationalites = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nationalites.Add(reader["nationalite"].ToString());
                }
            }
            else
            {
                if (value == null)
                {
                    nationalites = new List<string>();
                }
                else
                {
                    nationalites = value;
                }

            }

            return nationalites;
        }
        private List<string> parseAdresse(SqlDataReader reader, List<string> value)
        {
            List<string> adresses = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adresses.Add(reader["adresse"].ToString());
                }
            }
            else
            {
                if (value == null)
                {
                    adresses = new List<string>();
                }
                else
                {
                    adresses = value;
                }
            }

            return adresses;
        }
        #endregion

        #region Parse migrant DataReader
        #endregion

        #region Search migrant
        public static List<migrant> searchMigrant(string query, List<migrant> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant WHERE numero_migrant LIKE('%?%') OR nom_migrant LIKE('%?%') 
                            OR prenom_migrant LIKE('%?%') OR surnom_migrant LIKE('%?%') OR ethnicite_migrant LIKE('%?%') 
                            OR dat2naiss_migrant LIKE('%?%') OR nationalite_migrant LIKE('%?%')";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(query, query, query, query, query, query, query);

                return parse(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        #endregion

        public static List<migrant> parse(SqlDataReader reader, List<migrant> value)
        {
            List<migrant> migrants = new List<migrant>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    migrant migrant = new migrant();

                    migrant.id_migrant = int.Parse(reader["id_migrant"].ToString());
                    migrant.code_migrant = reader["numero_migrant"].ToString();
                    migrant.photo_migrant = reader["photo_migrant"].ToString();
                    migrant.nom_migrant = reader["nom_migrant"].ToString();
                    migrant.prenom_migrant = reader["prenom_migrant"].ToString();
                    migrant.surnom_migrant = reader["surnom_migrant"].ToString();
                    migrant.ethnicite_migrant = reader["ethnicite_migrant"].ToString();
                    migrant.sexe_migrant = reader["sexe_migrant"].ToString();
                    migrant.dat2naiss_migrant = reader["dat2naiss_migrant"].ToString();
                    migrant.date_enreg_migrant = reader["date_enreg_migrant"].ToString();
                    migrant.pas2piece = int.Parse(reader["pas2piece"].ToString());
                    migrant.cin_migrant = reader["cin_migrant"].ToString();
                    migrant.passeport_migrant = int.Parse(reader["passeport_migrant"].ToString());
                    migrant.permis2cond_migrant = reader["permis2cond_migrant"].ToString();
                    migrant.nif_migrant = reader["nif_migrant"].ToString();
                    migrant.description_vestim = reader["description_vestim"].ToString();
                    migrant.remarques = reader["remarques"].ToString();

                    migrant.num_tel_migrant = migrant.num_tel_migrant;
                    migrant.adress_migrant = migrant.adress_migrant;
                    migrant.nationalite_migrant = migrant.nationalite_migrant;

                    migrants.Add(migrant);
                }
            }
            else
            {
                if (value == null)
                {
                    migrants = new List<migrant>();
                }
                else
                {
                    migrants = value;
                }
            }



            return migrants;
        }

        public static Dictionary<string, string> addMigrant(migrant migrant/*, List<string> telephones, List<string> nationalites, List<string> adresses*/)
        {
            int ID = Utils.getCurrID("migrant");
            if (ID < 1)
            {
                return null;
            }
            string code = KeyGenerator.GetUniqueKey(6, true);
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                #region Old Code
                string sql = @"INSERT INTO migrant
                            (numero_migrant
                            ,photo_migrant
                            ,nom_migrant
                            ,prenom_migrant
                            ,surnom_migrant
                            ,ethnicite_migrant
                            ,sexe_migrant
                            ,dat2naiss_migrant
                            ,pas2piece
                            ,cin_migrant
                            ,passeport_migrant
                            ,permis2cond_migrant
                            ,nif_migrant
                            ,description_vestim
                            ,remarques
                            ,date_enreg_migrant)
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
                            ,?)";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    code.ToString(),
                    migrant.photo_migrant,
                    migrant.nom_migrant,
                    migrant.prenom_migrant,
                    migrant.surnom_migrant,
                    migrant.ethnicite_migrant,
                    migrant.sexe_migrant,
                    migrant.dat2naiss_migrant,
                    migrant.pas2piece,
                    migrant.cin_migrant,
                    migrant.passeport_migrant,
                    migrant.permis2cond_migrant,
                    migrant.nif_migrant,
                    migrant.description_vestim,
                    migrant.remarques,
                    migrant.date_enreg_migrant
                    );
                #endregion
                if (stmt.ExecuteNonQuery() > 0)
                {
                    dict.Add("id_migrant", ID.ToString());
                    dict.Add("numero_migrant", code);
                }
                return dict;
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
    }
}
