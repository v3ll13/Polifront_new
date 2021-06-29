namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class victimes
    {
        public int id_victimes { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        public string nom_vict { get; set; }
        public string prenom_vict { get; set; }
        private string _date2naiss_vict;
        public string date2naiss_vict
        {
            get
            {
                return _date2naiss_vict;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        var date_naissance = DateTime.Parse(value);
                        _date2naiss_vict = date_naissance.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _date2naiss_vict = null;
                    }
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                }


            }
        }
        private int _age_vict;
        public int age_vict
        {
            get
            {
                return _age_vict;
            }
            set
            {
                try
                {
                    if (date2naiss_vict != null)
                    {
                        _age_vict = DateTime.Now.Year - DateTime.ParseExact(date2naiss_vict, "dd-MM-yyyy", CultureInfo.InvariantCulture).Year - (DateTime.ParseExact(date2naiss_vict, "dd-MM-yyyy", CultureInfo.InvariantCulture).DayOfYear < DateTime.Now.DayOfYear ? 0 : 1);
                    }
                    else
                    {
                        age_vict = 0;
                    }
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                }

            }
        }
        public string sexe_vict { get; set; }
        private string _date_enreg_vict;
        public string date_enreg_vict
        {
            get
            {
                return _date_enreg_vict;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        var date_naissance = DateTime.Parse(value);
                        _date_enreg_vict = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _date_enreg_vict = null;
                    }
                }
                catch (Exception e)
                {
                    Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
                }

            }
        }

        public victimes(DataRow row)
        {
            try
            {
                parse(row);
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
            }

        }
        private victimes parse(DataRow row)
        {
            try
            {
                id_victimes = int.Parse(row["id_victimes"].ToString());
                id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
                numero_interp = row["numero_interp"].ToString();
                numero_migrant = row["numero_migrant"].ToString();
                nom_vict = row["nom_vict"].ToString();
                prenom_vict = row["prenom_vict"].ToString();
                date2naiss_vict = row["date2naiss_vict"].ToString();
                sexe_vict = row["sexe_vict"].ToString();
                date_enreg_vict = row["date_enreg_vict"].ToString();
                age_vict = age_vict;
            }
            catch (Exception e)
            {
                Utils.ErrorLogging(System.Reflection.MethodBase.GetCurrentMethod().Name, e);
            }

            return this;
        }

        public static int addVictime(victimes vic)
        {
            try
            {
                string sql = @"INSERT INTO victimes
                                (id_utilisateurs
                                ,numero_interp
                                ,numero_migrant
                                ,nom_vict
                                ,prenom_vict
                                ,date2naiss_vict
                                ,sexe_vict
                                ,date_enreg_vict)
                            VALUES
                                (?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,?
                                ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    vic.id_utilisateurs,
                    vic.numero_interp,
                    vic.numero_migrant,
                    vic.nom_vict,
                    vic.prenom_vict,
                    vic.date2naiss_vict,
                    vic.sexe_vict);
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
