using Db_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace POLIFRONT_WEB_API.App_Code
{
    public partial class utilisateur
    {
        public int id_utilisateurs { get; set; }
        public string matricule_agent { get; set; }
        public string nom_utils { get; set; }
        public string prenom_utils { get; set; }
        public string sexe_utils { get; set; }
        private string _dat2naiss_utils;
        public string dat2naiss_utils
        {
            get
            {
                return _dat2naiss_utils.ToString();
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _dat2naiss_utils = date_naissance.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    _dat2naiss_utils = null;
                }

            }
        }
        public string grade_agent { get; set; }
        public string email_utils { get; set; }
        public string mot2pass_utils { get; set; }
        public string typ_compte { get; set; }
        public int is_active_utils { get; set; }
        private string _date_enreg_utils;
        public string date_enreg_utils
        {
            get
            {
                return _date_enreg_utils;
            }
            set
            {
                if (value != null)
                {
                    var date_insert = DateTime.Parse(value);
                    _date_enreg_utils = date_insert.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_utils = null;
                }

            }
        }
        public string affectation { get; set; }
        public int is_first_login { get; set; }
        public string mot2pass_utils_defaut { get; set; }

        public static utilisateur login(string email, string password)
        
        {
            try
            {
                string sql = @"select * from utilisateurs where email_utils=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameter(0, email);
                SqlDataReader reader = stmt.ExecuteReader();

                utilisateur user = parseSingle(reader);
                if (user.is_first_login > 0)
                {
                    if (Utils.VerifyHashedPassword(user.mot2pass_utils_defaut, password))
                    {
                        return user;
                    }
                }
                else
                {
                    if (Utils.VerifyHashedPassword(user.mot2pass_utils, password))
                    {
                        return user;
                    }
                }

                return new utilisateur();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                Console.WriteLine(e);
                return null;
            }
        }

        public static int updatePassword(string email, string pass)
        {
            try
            {
                string sql = @"UPDATE utilisateurs SET mot2pass_utils=?, is_first_login=0 WHERE email_utils=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(Utils.HashPassword(pass), email);
                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return -1;
            }
        }

        public static int createUser(utilisateur user)
        {
            try
            {
                string sql =
                    @"INSERT INTO utilisateurs
                    (matricule_agent
                    ,nom_utils
                    ,prenom_utils
                    ,sexe_utils
                    ,dat2naiss_utils
                    ,grade_agent
                    ,email_utils
                    ,typ_compte
                    ,date_enreg_utils
                    ,mot2pass_utils_defaut
                    ,affectation)
                VALUES
                    (?
                    ,?
                    ,?
                    ,?
                    ,?
                    ,?
                    ,?
                    ,?
                    ,GETDATE()
                    ,?
                    ,?)";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    user.matricule_agent.ToUpper(),
                    user.nom_utils,
                    user.prenom_utils,
                    user.sexe_utils,
                    user.dat2naiss_utils,
                    user.grade_agent,
                    user.email_utils,
                    user.typ_compte,
                    Utils.HashPassword(user.mot2pass_utils_defaut),
                    user.affectation);
                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                Console.WriteLine(e.Message);
                return -1;
                
            }
        }

        private static utilisateur parseSingle(SqlDataReader reader)
        {
            if (reader.Read())
            {
                utilisateur user = new utilisateur();

                user.id_utilisateurs = int.Parse(reader["id_utilisateurs"].ToString());
                user.matricule_agent = reader["matricule_agent"].ToString();
                user.nom_utils = reader["nom_utils"].ToString();
                user.prenom_utils = reader["prenom_utils"].ToString();
                user.sexe_utils = reader["sexe_utils"].ToString();
                user.dat2naiss_utils = reader["dat2naiss_utils"].ToString();
                user.grade_agent = reader["grade_agent"].ToString();
                user.email_utils = reader["email_utils"].ToString();
                user.mot2pass_utils = reader["mot2pass_utils"].ToString();
                user.typ_compte = reader["typ_compte"].ToString();
                user.affectation = reader["affectation"].ToString();
                user.is_active_utils = int.Parse(reader["is_active_utils"].ToString());
                user.is_first_login = int.Parse(reader["is_first_login"].ToString());
                user.date_enreg_utils = reader["date_enreg_utils"].ToString();
                user.mot2pass_utils_defaut = reader["mot2pass_utils_defaut"].ToString();

                return user;
            }
            else
            {
                return new utilisateur();
            }
        }

        public static List<utilisateur> findUsers()
        {
            try
            {
                string sql = @"select * from utilisateurs";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                SqlDataReader reader = stmt.ExecuteReader();

                return parse(reader);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        public static utilisateur find(int id)
        {
            try
            {
                string sql = @"select * from utilisateurs where id_utilisateurs=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id);
                SqlDataReader reader = stmt.ExecuteReader();

                return parseSingle(reader);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }

        private static List<utilisateur> parse(SqlDataReader reader)
        {
            List<utilisateur> users = new List<utilisateur>();
            while (reader.Read())
            {
                utilisateur user = new utilisateur();

                user.id_utilisateurs = int.Parse(reader["id_utilisateurs"].ToString());
                user.matricule_agent = reader["matricule_agent"].ToString();
                user.nom_utils = reader["nom_utils"].ToString();
                user.prenom_utils = reader["prenom_utils"].ToString();
                user.sexe_utils = reader["sexe_utils"].ToString();
                user.dat2naiss_utils = reader["dat2naiss_utils"].ToString();
                user.grade_agent = reader["grade_agent"].ToString();
                user.email_utils = reader["email_utils"].ToString();
                user.typ_compte = reader["typ_compte"].ToString();
                //user.mot2pass_utils = reader["mot2pass_utils"].ToString();
                //user.mot2pass_utils_defaut = reader["mot2pass_utils_defaut"].ToString();
                user.affectation = reader["affectation"].ToString();
                user.is_active_utils = int.Parse(reader["is_active_utils"].ToString());
                user.is_first_login = int.Parse(reader["is_first_login"].ToString());
                user.date_enreg_utils = reader["date_enreg_utils"].ToString();

                users.Add(user);
            }
            return users;
        }
    }
}
