namespace POLIFRONT_WEB_API.App_Code
{
    using Db_Core;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public partial class ass_meur
    {
        public int id_ass_meur { get; set; }
        public int id_utilisateurs { get; set; }
        public string numero_interp { get; set; }
        public string numero_migrant { get; set; }
        private string _date_heure_asm;
        public string date_heure_asm
        {
            get
            {
                return _date_heure_asm;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_heure_asm = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_heure_asm = null;
                }

            }
        }
        public string departement_asm { get; set; }
        public string point_asm { get; set; }
        public string localite_asm { get; set; }
        public string circonstance_asm { get; set; }
        public int nbr2vitim_asm { get; set; }
        public string details_victim_asm { get; set; }
        public int is_judge { get; set; }
        public string nom_juge { get; set; }
        public string rue_asm { get; set; }
        public string commune_asm { get; set; }
        public string ville_asm { get; set; }
        public string medicaux_legal_asm { get; set; }
        public string morgue_asm { get; set; }
        public string description_asm { get; set; }
        private string _date_enreg_asm;
        public string date_enreg_asm
        {
            get
            {
                return _date_enreg_asm;
            }
            set
            {
                if (value != null)
                {
                    var date_naissance = DateTime.Parse(value);
                    _date_enreg_asm = date_naissance.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    _date_enreg_asm = null;
                }

            }
        }

        public ass_meur(DataRow row)
        {
            parse(row);
        }

        private ass_meur parse(DataRow row)
        {
            id_ass_meur = int.Parse(row["id_ass_meur"].ToString());
            id_utilisateurs = int.Parse(row["id_utilisateurs"].ToString());
            numero_interp = row["numero_interp"].ToString();
            numero_migrant = row["numero_migrant"].ToString();
            date_heure_asm = row["date_heure_asm"].ToString();
            departement_asm = row["departement_asm"].ToString();
            point_asm = row["point_asm"].ToString();
            localite_asm = row["localite_asm"].ToString();
            circonstance_asm = row["circonstance_asm"].ToString();
            nbr2vitim_asm = int.Parse(row["nbr2vitim_asm"].ToString());
            details_victim_asm = row["details_victim_asm"].ToString();
            is_judge = int.Parse(row["is_judge"].ToString());
            nom_juge = row["nom_juge"].ToString();
            rue_asm = row["rue_asm"].ToString();
            commune_asm = row["commune_asm"].ToString();
            ville_asm = row["ville_asm"].ToString();
            medicaux_legal_asm = row["medicaux_legal_asm"].ToString();
            morgue_asm = row["morgue_asm"].ToString();
            description_asm = row["description_asm"].ToString();
            date_enreg_asm = row["date_enreg_asm"].ToString();
            return this;
        }

        public static int addAssMeur(ass_meur ass)
        {
            try
            {
                string sql = @"INSERT INTO dbo.ass_meur
                            (id_utilisateurs
                            ,numero_interp
                            ,numero_migrant
                            ,date_heure_asm
                            ,departement_asm
                            ,point_asm
                            ,localite_asm
                            ,circonstance_asm
                            ,nbr2vitim_asm
                            ,details_victim_asm
                            ,is_judge
                            ,nom_juge
                            ,rue_asm
                            ,commune_asm
                            ,ville_asm
                            ,medicaux_legal_asm
                            ,morgue_asm
                            ,description_asm
                            ,date_enreg_asm)
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
                            ,?
                            ,?
                            ,GETDATE())";
                SqlStatement stmt = SqlStatement.FromString(sql, SqlConnString.POLIFRONT);
                stmt.SetParameters(
                    ass.id_utilisateurs,
                    ass.numero_interp,
                    ass.numero_migrant,
                    ass.date_heure_asm,
                    ass.departement_asm,
                    ass.point_asm,
                    ass.localite_asm,
                    ass.circonstance_asm,
                    ass.nbr2vitim_asm,
                    ass.details_victim_asm,
                    ass.is_judge,
                    ass.nom_juge,
                    ass.rue_asm,
                    ass.commune_asm,
                    ass.ville_asm,
                    ass.medicaux_legal_asm,
                    ass.morgue_asm,
                    ass.description_asm);
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
