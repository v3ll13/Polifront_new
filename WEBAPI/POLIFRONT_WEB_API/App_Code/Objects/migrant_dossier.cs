using Db_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace POLIFRONT_WEB_API.App_Code.Objects
{
    public class migrant_dossier
    {
        /*
          [id_mig_interp_doss]
          [id_utilisateurs]
          [numero_migrant]
          [photo_migrant]
          [nom_migrant]
          [prenom_migrant]
          [surnom_migrant]
          [ethnicite_migrant]
          [sexe_migrant]
          [dat2naiss_migrant]
          [description_vestim]
          [remarques]
          [date_enreg_migrant]
         */

        public int id_mig_interp_doss { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_migrant { get; set; }
        //public string code_dossier { get; set; }
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
                return _dat2naiss_migrant;
            }
            set
            {
                bool check = value != null && !value.Equals("");
                if (check)
                {
                    DateTime date_naissance;
                    try
                    {
                        date_naissance = DateTime.Parse(value, CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            date_naissance = DateTime.ParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception ex)
                        {
                            date_naissance = DateTime.ParseExact(value, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        }

                    }


                    _dat2naiss_migrant = date_naissance.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    _dat2naiss_migrant = "";
                }

            }
        }
        private int _age_migrant;
        public int age_migrant
        {
            get
            {
                return _age_migrant;
            }
            set
            {
                if (!dat2naiss_migrant.Equals(""))
                {
                    _age_migrant = DateTime.Now.Year - DateTime.ParseExact(dat2naiss_migrant, "dd-MM-yyyy", CultureInfo.InvariantCulture).Year - (DateTime.ParseExact(dat2naiss_migrant, "dd-MM-yyyy", CultureInfo.InvariantCulture).DayOfYear < DateTime.Now.DayOfYear ? 0 : 1);
                }
                else
                {
                    _age_migrant = 0;
                }
            }
        }
        private List<nationalite> _nationalite_migrant;
        public List<nationalite> nationalite_migrant
        {
            get
            {
                return _nationalite_migrant;
            }
            set
            {
                if (value == null || value.Count == 0)
                {
                    if (numero_migrant != null)
                    {
                        _nationalite_migrant = getListNationalite(numero_migrant, value);
                    }
                    else
                    {
                        _nationalite_migrant = new List<nationalite>();
                    }
                }
                else
                {
                    _nationalite_migrant = value;
                }
            }
        }
        private List<piece> _piece_migrant;
        public List<piece> piece_migrant
        {
            get
            {
                return _piece_migrant;
            }
            set
            {
                if (value == null || value.Count == 0)
                {
                    if (numero_migrant != null)
                    {
                        _piece_migrant = getListPiece(numero_migrant, value);
                    }
                    else
                    {
                        _piece_migrant = new List<piece>();
                    }
                }
                else
                {
                    _piece_migrant = value;
                }
            }
        }

        private List<adresse> _adress_migrant;
        public List<adresse> adress_migrant
        {
            get
            {
                return _adress_migrant;
            }
            set
            {
                if (value == null || value.Count == 0)
                {
                    if (numero_migrant != null)
                    {
                        _adress_migrant = getListAdresse(numero_migrant, value);
                    }
                    else
                    {
                        _adress_migrant = new List<adresse>();
                    }
                }
                else
                {
                    _adress_migrant = value;
                }
            }
        }
        private List<telephone> _num_tel_migrant;
        public List<telephone> num_tel_migrant
        {
            get
            {
                return _num_tel_migrant;
            }
            set
            {
                if (value == null || value.Count == 0)
                {
                    if (numero_migrant != null)
                    {
                        _num_tel_migrant = getListTelephone(numero_migrant, value);
                    }
                    else
                    {
                        _num_tel_migrant = new List<telephone>();
                    }
                }
                else
                {
                    _num_tel_migrant = value;
                }
            }
        }
        private List<Dictionary<string, object>> _type_interp;
        public List<Dictionary<string, object>> type_interp
        {
            get
            {
                return _type_interp;
            }
            set
            {
                if (value == null || value.Count == 0)
                {
                    if (numero_migrant != null)
                    {
                        _type_interp = getListInterp(numero_migrant, value);
                    }
                    else
                    {
                        _type_interp = new List<Dictionary<string, object>>();
                    }
                }
                else
                {
                    _type_interp = value;
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
                if (value != null && !value.Equals(""))
                {
                    var date_insert = DateTime.Parse(value);
                    _date_enreg_migrant = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_migrant = "";
                }

            }
        }
        public int statut { get; set; }
        //public int nombre_charge { get; set; }

        #region List infos migrant
        private List<telephone> getListTelephone(string numero_migrant, List<telephone> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_telephone WHERE numero_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(numero_migrant);
                return parseTelephone(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<nationalite> getListNationalite(string numero_migrant, List<nationalite> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_nationalite WHERE numero_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(numero_migrant);
                return parseNationalite(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<adresse> getListAdresse(string numero_migrant, List<adresse> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_adresse WHERE numero_migrant = ?";

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(numero_migrant);
                return parseAdresse(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<piece> getListPiece(string numero_migrant, List<piece> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_piece WHERE numero_migrant = ?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(numero_migrant);
                return parsePiece(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        private List<Dictionary<string, object>> getListInterp(string numero_migrant, List<Dictionary<string, object>> value)
        {
            List<Dictionary<string, object>> interps = new List<Dictionary<string, object>>();
            List<string> tables = Utils.tableList;
            for (int i = 0; i < tables.Count; i++)
            {
                try
                {
                    string sql = @"SELECT * FROM " + tables[i] + " WHERE numero_migrant=?";
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                    stmt.SetParameters(numero_migrant);
                    //SqlDataReader reader = stmt.ExecuteReader();
                    //Dictionary<string, object> interp = new Dictionary<string, object>();
                    //object obj = GetInstance("POLIFRONT_WEB_API.App_Code." + tables[i], reader);
                    //interp.Add(tables[i], obj);
                    //interps.Add(interp);

                    DataTable table = SqlConvert.ToDataTable(stmt.ExecuteReader());
                    Dictionary<string, object> interp = new Dictionary<string, object>();
                    object obj = GetInstance("POLIFRONT_WEB_API.App_Code." + tables[i], table);
                    if (obj == null)
                    {
                        continue;
                    }
                    //nombre_charge++;
                    interp.Add(tables[i], obj);
                    interps.Add(interp);

                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name + " | " + tables[i], e);
                    //return null;
                    continue;
                }
                finally
                {
                    //
                }
            }
            return interps;

        }

        public object GetInstance(string strFullyQualifiedName, SqlDataReader reader)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t, reader);
        }
        public object GetInstance(string strFullyQualifiedName, DataTable table)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            Array array = Array.CreateInstance(t, table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                array.SetValue(Activator.CreateInstance(t, table.Rows[i]), i);
            }

            Type genericListType = typeof(List<>);
            Type concreteListType = genericListType.MakeGenericType(t);
            object list = Activator.CreateInstance(concreteListType, new Object[] { array });
            return list;
            //return Activator.CreateInstance(t, table);
        }

        private List<telephone> parseTelephone(SqlDataReader reader, List<telephone> value)
        {
            List<telephone> phones = new List<telephone>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    telephone telephone = new telephone();
                    telephone.id_telephone = int.Parse(reader["id_telephone"].ToString());
                    telephone.numero_migrant = reader["numero_migrant"].ToString();
                    telephone.area_code = reader["area_code"].ToString();
                    telephone.telephone_migrant = reader["telephone_migrant"].ToString();
                    telephone.date_enreg_tel = reader["date_enreg_tel"].ToString();
                    phones.Add(telephone);
                }
            }
            else
            {
                if (value == null)
                {
                    phones = new List<telephone>();
                }
                else
                {
                    phones = value;
                }
            }

            return phones;
        }
        private List<nationalite> parseNationalite(SqlDataReader reader, List<nationalite> value)
        {
            List<nationalite> nationalites = new List<nationalite>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nationalite nationalite = new nationalite();
                    nationalite.id_nationalite = int.Parse(reader["id_nationalite"].ToString());
                    nationalite.numero_migrant = reader["numero_migrant"].ToString();
                    nationalite.nationalite_migrant = reader["nationalite_migrant"].ToString();
                    nationalite.date_enreg_nation = reader["date_enreg_nation"].ToString();
                    nationalites.Add(nationalite);
                }
            }
            else
            {
                if (value == null)
                {
                    nationalites = new List<nationalite>();
                }
                else
                {
                    nationalites = value;
                }

            }

            return nationalites;
        }
        private List<adresse> parseAdresse(SqlDataReader reader, List<adresse> value)
        {
            List<adresse> adresses = new List<adresse>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    adresse adresse = new adresse();
                    adresse.id_adresse = int.Parse(reader["id_adresse"].ToString());
                    adresse.numero_migrant = reader["numero_migrant"].ToString();
                    adresse.adresse_migrant = reader["adresse_migrant"].ToString();
                    adresse.date_enreg_adress = reader["date_enreg_adress"].ToString();
                    adresses.Add(adresse);
                }
            }
            else
            {
                if (value == null)
                {
                    adresses = new List<adresse>();
                }
                else
                {
                    adresses = value;
                }
            }

            return adresses;
        }
        private List<piece> parsePiece(SqlDataReader reader, List<piece> value)
        {
            List<piece> pieces = new List<piece>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    piece piece = new piece();
                    piece.id_pieces = int.Parse(reader["id_pieces"].ToString());
                    piece.numero_migrant = reader["numero_migrant"].ToString();
                    piece.type_piece = reader["type_piece"].ToString();
                    piece.identification_migrant = reader["identification_migrant"].ToString();
                    piece.pays_piece = reader["pays_piece"].ToString();
                    pieces.Add(piece);
                }
            }
            else
            {
                if (value == null)
                {
                    pieces = new List<piece>();
                }
                else
                {
                    pieces = value;
                }
            }

            return pieces;
        }
        #endregion

        public static List<migrant_dossier> parse(SqlDataReader reader, List<migrant_dossier> value)
        {
            List<migrant_dossier> migrants = new List<migrant_dossier>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    migrant_dossier migrant = new migrant_dossier();

                    migrant.id_mig_interp_doss = int.Parse(reader["id_mig_interp_doss"].ToString());
                    migrant.id_utilisateurs = int.Parse(reader["id_utilisateurs"].ToString());
                    migrant.numero_migrant = reader["numero_migrant"].ToString();
                    //migrant.code_dossier = reader["code_dossier"].ToString();
                    migrant.photo_migrant = reader["photo_migrant"].ToString();
                    migrant.nom_migrant = reader["nom_migrant"].ToString();
                    migrant.prenom_migrant = reader["prenom_migrant"].ToString();
                    migrant.surnom_migrant = reader["surnom_migrant"].ToString();
                    migrant.ethnicite_migrant = reader["ethnicite_migrant"].ToString();
                    migrant.sexe_migrant = reader["sexe_migrant"].ToString();
                    migrant.dat2naiss_migrant = reader["dat2naiss_migrant"].ToString();
                    migrant.date_enreg_migrant = reader["date_enreg_migrant"].ToString();
                    //migrant.pas2piece = int.Parse(reader["pas2piece"].ToString());
                    //migrant.cin_migrant = reader["cin_migrant"].ToString();
                    //migrant.passeport_migrant = int.Parse(reader["passeport_migrant"].ToString());
                    //migrant.permis2cond_migrant = reader["permis2cond_migrant"].ToString();
                    //migrant.nif_migrant = reader["nif_migrant"].ToString();
                    migrant.description_vestim = reader["description_vestim"].ToString();
                    migrant.remarques = reader["remarques"].ToString();
                    migrant.statut = int.Parse(reader["statut"].ToString());

                    migrant.age_migrant = migrant.age_migrant;
                    migrant.num_tel_migrant = migrant.num_tel_migrant;
                    migrant.adress_migrant = migrant.adress_migrant;
                    migrant.nationalite_migrant = migrant.nationalite_migrant;
                    migrant.piece_migrant = migrant.piece_migrant;
                    migrant.type_interp = migrant.type_interp;

                    migrants.Add(migrant);
                }
            }
            else
            {
                if (value == null)
                {
                    migrants = new List<migrant_dossier>();
                }
                else
                {
                    migrants = value;
                }
            }
            return migrants;
        }

        public static int closeEditDossier(int id)
        {
            try
            {
                string sql = @"UPDATE migrant_interp_dossier SET statut=1 WHERE id_mig_interp_doss=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id);
                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }

        public static Dictionary<string, string> nouveauDossier(string id_user, string affectation)
        {
            int ID = Utils.getCurrID("migrant_interp_dossier");
            if (ID < 1)
            {
                return null;
            }
            string code = KeyGenerator.GenerateCodeDossier(affectation);
            if (code == null)
            {
                return null;
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                string sql = @"INSERT INTO migrant_interp_dossier(numero_migrant, id_utilisateurs, date_enreg_migrant)
                            VALUES(?, ?, GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(code, id_user);

                if (stmt.ExecuteNonQuery() > 0)
                {
                    dict.Add("id_mig_interp_doss", ID.ToString());
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

        public static Dictionary<string, string> addMigrant(migrant_dossier migrant/*, List<string> telephones, List<string> nationalites, List<string> adresses*/)
        {
            int ID = Utils.getCurrID("migrant_interp_dossier");
            if (ID < 1)
            {
                return null;
            }
            string code = KeyGenerator.GetUniqueKey(6, true);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {

                string sql = @"INSERT INTO migrant_interp_dossier
                            (id_utilisateurs
                            ,numero_migrant
                            ,photo_migrant
                            ,nom_migrant
                            ,prenom_migrant
                            ,surnom_migrant
                            ,ethnicite_migrant
                            ,sexe_migrant
                            ,dat2naiss_migrant
                            ,description_vestim
                            ,remarques
                            ,date_enreg_migrant
                            ,statut)
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
                            ,GETDATE()
                            ,2)";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    migrant.id_utilisateurs,
                    code,
                    migrant.photo_migrant,
                    migrant.nom_migrant,
                    migrant.prenom_migrant,
                    migrant.surnom_migrant,
                    migrant.ethnicite_migrant,
                    migrant.sexe_migrant,
                    migrant.dat2naiss_migrant,
                    migrant.description_vestim,
                    migrant.remarques);

                int result = stmt.ExecuteNonQuery();
                if (result > 0)
                {
                    result += addMigrantAdresse(migrant);
                    result += addMigrantPhone(migrant);
                    result += addMigrantNationalite(migrant);
                    result += addMigrantPiece(migrant);
                }


                if (result > 0)
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

        public static int addMigrantPhone(migrant_dossier migrant)
        {
            List<telephone> phones = migrant.num_tel_migrant;
            if (phones.Count > 0)
            {
                cleanMigrantInfos(migrant.numero_migrant, "migrant_telephone");
                string sql = @"INSERT INTO migrant_telephone(numero_migrant, area_code, telephone_migrant, date_enreg_tel) VALUES";
                for (int i = 0; i < phones.Count; i++)
                {
                    sql += "('" + migrant.numero_migrant + "','', '" + phones[i].numero_migrant + "', GETDATE()),";
                }
                sql = sql.Remove(sql.Length - 1);
                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                    return stmt.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public static int addMigrantNationalite(migrant_dossier migrant)
        {
            List<nationalite> nationalites = migrant.nationalite_migrant;
            if (nationalites.Count > 0)
            {
                cleanMigrantInfos(migrant.numero_migrant, "migrant_nationalite");
                string sql = @"INSERT INTO migrant_nationalite(numero_migrant, nationalite_migrant, date_enreg_nation) VALUES";
                for (int i = 0; i < nationalites.Count; i++)
                {
                    sql += "('" + migrant.numero_migrant + "', '" + Regex.Replace(nationalites[i].nationalite_migrant, @"([|\\*])", @"\$1").Replace("'", "''") + "', GETDATE()),";
                }
                sql = sql.Remove(sql.Length - 1);
                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                    return stmt.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public static int addMigrantAdresse(migrant_dossier migrant)
        {
            List<adresse> adresses = migrant.adress_migrant;
            if (adresses.Count > 0)
            {
                cleanMigrantInfos(migrant.numero_migrant, "migrant_adresse");
                string sql = @"INSERT INTO migrant_adresse(numero_migrant, adresse_migrant, date_enreg_adress) VALUES";
                for (int i = 0; i < adresses.Count; i++)
                {
                    sql += "('" + migrant.numero_migrant + "', '" + Regex.Replace(adresses[i].adresse_migrant, @"([|\\*])", @"\$1").Replace("'", "''") + "', GETDATE()),";
                }
                sql = sql.Remove(sql.Length - 1);
                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                    return stmt.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public static int addMigrantPiece(migrant_dossier migrant)
        {
            List<piece> pieces = migrant.piece_migrant;
            if (pieces.Count > 0)
            {
                cleanMigrantInfos(migrant.numero_migrant, "migrant_piece");
                string sql = @"INSERT INTO migrant_piece(numero_migrant, type_piece, identification_migrant, pays_piece) VALUES";
                for (int i = 0; i < pieces.Count; i++)
                {
                    piece p = pieces[i];
                    sql += "('" + migrant.numero_migrant + "', '" + Regex.Replace(p.type_piece, @"([|\\*])", @"\$1").Replace("'", "''") + "', '" + p.identification_migrant + "', '" + Regex.Replace(p.pays_piece, @"([|\\*])", @"\$1").Replace("'", "''") + "'),";
                }
                sql = sql.Remove(sql.Length - 1);
                try
                {
                    SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                    return stmt.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }

        private static int cleanMigrantInfos(string numero_migrant, string table_name)
        {
            try
            {
                string sql = @"DELETE FROM " + table_name + " WHERE numero_migrant=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(numero_migrant);
                return stmt.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static List<migrant_dossier> getMigrants(string id, int offset, int max)
        {
            try
            {
                string sql = @"SELECT * FROM migrant_interp_dossier ";
                if (id != null && !id.Equals(""))
                {
                    sql += "WHERE id_mig_interp_doss = '" + id + "' ";
                }
                sql += "ORDER BY date_enreg_migrant ASC " +
                    "OFFSET " + offset + " ROWS " +
                    "FETCH NEXT " + max + " ROWS ONLY";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                return parse(stmt.ExecuteReader(), null);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        internal static Dictionary<string, string> fillMigrantInfo(migrant_dossier migrant)
        {
            //Utils.Logging("MIGRANTTT", JsonConvert.SerializeObject(migrant));
            Dictionary<string, string> map = new Dictionary<string, string>();
            string code = KeyGenerator.GetUniqueKey(6, true);
            //Utils.Logging("GENERATED CODE", code);
            //Utils.Logging("MIGRANT OBJECT", JsonConvert.SerializeObject(migrant));
            try
            {
                //string sql = @"UPDATE migrant_interp_dossier
                //           SET numero_migrant = ?
                //              [,photo_migrant = ?]
                //              [,nom_migrant = ?]
                //              [,prenom_migrant = ?]
                //              [,surnom_migrant = ?]
                //              [,ethnicite_migrant = ?]
                //              ,sexe_migrant = ?
                //              [,dat2naiss_migrant = ?]
                //              [,description_vestim = ?]
                //              [,remarques = ?]
                //         WHERE id_mig_interp_doss = ?";
                string sql = "UPDATE migrant_interp_dossier SET statut = '" + migrant.statut + "' ";

                //stmt.SetParameter(0, code);
                if (migrant.statut != 2)
                {
                    sql += " , numero_migrant = '" + code + "'";
                }
                if (migrant.photo_migrant != null && !migrant.photo_migrant.Equals(""))
                {
                    //stmt.SetParameter(1, migrant.photo_migrant);
                    sql += " , photo_migrant = '" + migrant.photo_migrant + "'";
                }
                if (migrant.nom_migrant != null && !migrant.nom_migrant.Equals(""))
                {
                    //stmt.SetParameter(2, migrant.nom_migrant);
                    sql += " , nom_migrant = '" + migrant.nom_migrant + "'";
                }
                if (migrant.prenom_migrant != null && !migrant.prenom_migrant.Equals(""))
                {
                    //stmt.SetParameter(3, migrant.prenom_migrant);
                    sql += " , prenom_migrant = '" + migrant.prenom_migrant + "'";
                }
                if (migrant.surnom_migrant != null && !migrant.surnom_migrant.Equals(""))
                {
                    //stmt.SetParameter(4, migrant.surnom_migrant);
                    sql += " , surnom_migrant = '" + migrant.surnom_migrant + "'";
                }
                if (migrant.ethnicite_migrant != null && !migrant.ethnicite_migrant.Equals(""))
                {
                    //stmt.SetParameter(5, migrant.ethnicite_migrant);
                    sql += " , ethnicite_migrant = '" + migrant.ethnicite_migrant + "'";
                }
                //stmt.SetParameter(1, migrant.sexe_migrant);
                sql += " , sexe_migrant = '" + migrant.sexe_migrant + "'";
                if (migrant.dat2naiss_migrant != null && !migrant.dat2naiss_migrant.Equals(""))
                {
                    //stmt.SetParameter(6, migrant.dat2naiss_migrant);
                    sql += " , dat2naiss_migrant = '" + DateTime.ParseExact(migrant.dat2naiss_migrant, "dd-MM-yyyy", CultureInfo.InvariantCulture) + "'";
                }
                if (migrant.description_vestim != null && !migrant.description_vestim.Equals(""))
                {
                    //stmt.SetParameter(7, migrant.description_vestim);
                    sql += " , description_vestim = '" + migrant.description_vestim + "'";
                }
                if (migrant.remarques != null && !migrant.remarques.Equals(""))
                {
                    //stmt.SetParameter(8, migrant.remarques);
                    sql += " , remarques = '" + migrant.remarques + "'";
                }
                //stmt.SetParameter(9, migrant.id_mig_interp_doss);
                sql += " WHERE id_mig_interp_doss = '" + migrant.id_mig_interp_doss + "'";

                //Utils.Logging("SQL QUERY", sql);

                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);

                int result = stmt.ExecuteNonQuery();
                if (result > 0)
                {
                    result += addMigrantAdresse(migrant);
                    result += addMigrantPhone(migrant);
                    result += addMigrantNationalite(migrant);
                    result += addMigrantPiece(migrant);
                }

                if (result > 0)
                {
                    map.Add("id_mig_interp_doss", migrant.id_mig_interp_doss.ToString());
                    map.Add("numero_migrant", code);
                }
                return map;

            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }

        public static int removeDraftDossier(int id)
        {
            try
            {
                string sql = @"DELETE FROM migrant_interp_dossier WHERE id_mig_interp_doss=? AND statut=0";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);

                stmt.SetParameters(id);

                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }

        #region Additionnal classes
        public class piece
        {
            public int id_pieces { get; set; }
            public string numero_migrant { get; set; }
            public string type_piece { get; set; }
            public string identification_migrant { get; set; }
            public string pays_piece { get; set; }
        }
        public class adresse
        {
            public int id_adresse { get; set; }
            public string numero_migrant { get; set; }
            public string adresse_migrant { get; set; }
            private string _date_enreg_adress;
            public string date_enreg_adress
            {
                get { return _date_enreg_adress; }
                set
                {
                    if (value != null && !value.Equals(""))
                    {
                        var date_insert = DateTime.Parse(value);
                        _date_enreg_adress = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _date_enreg_adress = "";
                    }
                }
            }
        }
        public class nationalite
        {
            public int id_nationalite { get; set; }
            public string numero_migrant { get; set; }
            public string nationalite_migrant { get; set; }
            private string _date_enreg_nation;
            public string date_enreg_nation {
                get { return _date_enreg_nation; }
                set
                {
                    if (value != null && !value.Equals(""))
                    {
                        var date_insert = DateTime.Parse(value);
                        _date_enreg_nation = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _date_enreg_nation = "";
                    }
                }
            }
        }
        public class telephone
        {
            public int id_telephone { get; set; }
            public string numero_migrant { get; set; }
            public string area_code { get; set; }
            public string telephone_migrant { get; set; }
            private string _date_enreg_tel;
            public string date_enreg_tel {
                get { return _date_enreg_tel; }
                set
                {
                    if (value != null && !value.Equals(""))
                    {
                        var date_insert = DateTime.Parse(value);
                        _date_enreg_tel = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _date_enreg_tel = "";
                    }
                }
            }
        }
        #endregion
    }
}