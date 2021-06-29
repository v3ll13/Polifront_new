using Db_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace POLIFRONT_WEB_API.App_Code.Objects
{
    public class dossier
    {
        //public int id_dossier { get; set; }
        //public string code_dossier { get; set; }
        //public string id_utilisateur { get; set; }
        //private string _date_enreg_dossier;
        //public string date_enreg_dossier
        //{
        //    get
        //    {
        //        return _date_enreg_dossier;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            var date_insert = DateTime.Parse(value);
        //            _date_enreg_dossier = date_insert.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
        //        }
        //        else
        //        {
        //            _date_enreg_dossier = null;
        //        }

        //    }
        //}
        //private string _date_modif_dossier;
        //public string date_modif_dossier
        //{
        //    get
        //    {
        //        return _date_modif_dossier;
        //    }
        //    set
        //    {
        //        if (value != null && !value.Trim().Equals(""))
        //        {
        //            var date_insert = DateTime.Parse(value);
        //            _date_modif_dossier = date_insert.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
        //        }
        //        else
        //        {
        //            _date_modif_dossier = null;
        //        }

        //    }
        //}
        //public int statut { get; set; }
        //private List<migrant> _migrants;
        //public List<migrant> migrants
        //{
        //    get
        //    {
        //        return _migrants;
        //    }
        //    set
        //    {
        //        _migrants = getListMigrant(id_dossier, value);
        //    }
        //}
        //public List<vehicules> vehiculess { get; set; }
        //public List<vehicules> vehicules = new List<vehicules>();
        //public List<suivi> suivis { get; set; }
        //public List<suivi> suivis = new List<suivi>();

        //private static string table_name = "dossier";

        #region List migrant
        private static List<migrant> getListMigrant(int id_dossier, List<migrant> value)
        {
            try
            {
                string sql = @"SELECT * FROM migrant WHERE id_migrant IN (
                            SELECT id_migrant FROM dossier_migrant WHERE id_dossier=?
                            )";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(id_dossier);
                return migrant.parse(stmt.ExecuteReader(), value);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }
        #endregion

        public static int matchDossierMigrant(string dossier, string migrant, int userID)
        {
            try
            {
                string sql = @"INSERT INTO dossier_migrant(id_dossier, id_migrant, match_date, id_utilisateur) 
                                VALUES(?, ?, getdate(), ?)";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(dossier, migrant, userID);

                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static Dictionary<string, string> nouveauDossier(string id_user, string affectation)
        {
            int ID = Utils.getCurrID("dossier");
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
                string sql = @"INSERT INTO dossier(code_dossier, id_utilisateur, date_enreg_dossier)
                            VALUES(?, ?, getDate())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(code, id_user);

                if (stmt.ExecuteNonQuery() > 0)
                {
                    dict.Add("id_dossier", ID.ToString());
                    dict.Add("code_dossier", code);
                }

                return dict;
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                return null;
            }
        }

        //public static List<dossier> getDossier(string id_dossier, int offset, int max)
        //{
        //    try
        //    {
        //        string sql = @"SELECT * FROM dossier ";
        //        if (id_dossier != null)
        //        {
        //            sql += "WHERE id_dossier = '" + id_dossier + "' ";
        //        }
        //        sql += "ORDER BY date_enreg_dossier desc " +
        //            "OFFSET " + offset + " ROWS " +
        //            "FETCH NEXT " + max + " ROWS ONLY";
        //        SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
        //        SqlDataReader reader = stmt.ExecuteReader();
        //        return parse(reader);
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
        //        return null;
        //    }
        //}

        //private static List<dossier> parse(SqlDataReader reader)
        //{
        //    List<dossier> dossiers = new List<dossier>();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            dossier dossier = new dossier();

        //            dossier.id_dossier = int.Parse(reader["id_dossier"].ToString());
        //            dossier.code_dossier = reader["code_dossier"].ToString();
        //            dossier.id_utilisateur = reader["id_utilisateur"].ToString();
        //            dossier.date_enreg_dossier = reader["date_enreg_dossier"].ToString();
        //            if (!reader["date_modif_dossier"].ToString().Trim().Equals(""))
        //            {
        //                dossier.date_modif_dossier = reader["date_modif_dossier"].ToString();
        //            }
        //            dossier.statut = int.Parse(reader["statut"].ToString());
        //            dossier.migrants = dossier.migrants;

        //            dossiers.Add(dossier);
        //        }
        //    }
        //    return dossiers;
        //}

        public static int removeDraftDossier(int id)
        {
            try
            {
                string sql = @"DELETE FROM dossier WHERE id_dossier=? AND statut=0";
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

        internal static int updateStatus(string container, int statut)
        {
            try
            {
                string sql = @"UPDATE dossier SET statut=? WHERE id_dossier=?";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(statut, container);
                return stmt.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}